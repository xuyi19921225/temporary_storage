using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Middlewares;
using BestPracticeWeb.WebApi.Repository;
using BestPracticeWeb.WebApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Text;

namespace BestPracticeWeb.WebApi
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

            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
                //options.Filters.Add(typeof(WebApiResultAttribute));
                options.Filters.Add<ExceptionAttribute>();
                //options.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson(options=> {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });


            //关闭自动验证 走过滤器进行验证
            services.Configure<ApiBehaviorOptions>(options =>
              options.SuppressModelStateInvalidFilter = true);

            services.Configure<LDAPOptions>(Configuration.GetSection("LDAP"));
            services.Configure<EncyptOptions>(Configuration.GetSection("Encypt"));
            services.Configure<JwtOptions>(Configuration.GetSection("Authentication:JwtBearer:JwtOptions"));
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<FilesOptions>(Configuration.GetSection("FilesOptions"));

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

            services.AddScoped<IJwtSerivce, JwtService>();
            services.AddScoped<ILDAPService, LDAPService>();
            services.AddScoped<IFileService, FileService>();

            #region 仓储服务注册
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISiteAreaRepository, SiteAreaRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ISAPUserRepository, SAPUserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectHistoryRepository, ProjectHistoryRepository>();
            #endregion

            #region 业务逻辑服务注册
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISiteAreaService, SiteAreaService>();
            services.AddScoped<ISAPUserService, SAPUserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectHistoryService, ProjectHistoryService>();
            #endregion

            #region Swaager
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BestPracticeWeb",
                    Description = "API",
                    Contact = new OpenApiContact() { Name = "XuYi", Email = "yi_xu5@jabil.com" }
                });

                // include document file
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "BestPracticeWeb.WebApi.xml"), true);


                #region Token绑定到ConfigureServices
                // 开启加权小锁
                option.OperationFilter<AddResponseHeadersFilter>();
                option.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //在header中添加Token
                option.OperationFilter<SecurityRequirementsOperationFilter>();
                //添加jwt定义
                option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description="JWT授权 格式：Bearer {token}",
                    Name="Authorization",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey
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

            app.UseDefaultFiles();

            //app.UseStaticFiles();
            FilesOptions filesOptions= Configuration.GetSection("FilesOptions").Get<FilesOptions>();

            app.UseStaticFiles(new StaticFileOptions() { FileProvider = new PhysicalFileProvider(filesOptions.BaseUrl)});

            app.UseHttpsRedirection();
     
            app.UseRouting();

            app.UseCors("MyAllowSpecificOrigins");
            // 开启认证
            app.UseAuthentication();
            // 开启授权
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
