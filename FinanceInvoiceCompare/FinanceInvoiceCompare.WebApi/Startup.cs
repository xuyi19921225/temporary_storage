using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.Common.DB;
using FinanceInvoiceCompare.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Service;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using Swashbuckle.AspNetCore.Filters;

namespace FinanceInvoiceCompare.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            services.AddControllers();

            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
                //options.Filters.Add(typeof(WebApiResultAttribute));
                options.Filters.Add<ExceptionAttribute>();
                //options.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson(options => {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.Configure<ApiBehaviorOptions>(options =>
             options.SuppressModelStateInvalidFilter = true);
            BaseDBConfig dbConfig = Configuration.GetSection("DBS").Get<BaseDBConfig>();

            //注册服务，使用Scope保证每个会话的实力不同
            services.AddScoped<SqlSugar.ISqlSugarClient>(o =>
            {
                return new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
                {
                    ConnectionString = dbConfig.Connection,//必填, 数据库连接字符串
                    DbType = (SqlSugar.DbType)dbConfig.DbType,//必填, 数据库类型
                    IsAutoCloseConnection = true,//默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                    InitKeyType = SqlSugar.InitKeyType.SystemTable//默认SystemTable, 字段信息读取, 如：该属性是不是主键，标识列等等信息
                });
            });

            #region 注册配置文件
            services.Configure<LDAPOptions>(Configuration.GetSection("LDAP"));
            services.Configure<JwtOptions>(Configuration.GetSection("Authentication:JwtBearer:JwtOptions"));
     
            #endregion


            #region 注册服务
            services.AddScoped<IJwtSerivce, JwtService>();
            services.AddScoped<ILDAPService, LDAPService>();
            #endregion

            #region 注册JWT验证的服务及参数
            JwtOptions jwtOptions = Configuration.GetSection("Authentication:JwtBearer:JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,//发行人
                    //是否验证受众人
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,//受众人
                    //是否验证密钥
                    ValidateIssuerSigningKey = true,
                    //密钥
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                    ValidateLifetime = true, //验证生命周期
                    RequireExpirationTime = true //过期时间
                };
            });
            #endregion


            #region 注册Swaager服务
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FinanceInvoiceCompare",
                    Description = "API",
                    Contact = new OpenApiContact() { Name = "XuYi", Email = "yi_xu5@jabil.com" }
                });

                // include document file
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "FinanceInvoiceCompare.WebApi.xml"), true);


                #region Token绑定到ConfigureServices
                // 开启加权小锁
                option.OperationFilter<AddResponseHeadersFilter>();
                option.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //在header中添加Token
                option.OperationFilter<SecurityRequirementsOperationFilter>();
                //添加jwt定义
                option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权 格式：Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("MyAllowSpecificOrigins");
            // 开启认证
            app.UseAuthentication();

            app.UseAuthorization();


            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            //Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 Docs");
                //路径配置，设置为空，表示直接访问该文件
                option.RoutePrefix = "";

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
