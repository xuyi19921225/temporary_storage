using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Common
{

    public class CustomSqlFunc
    {
        public static List<SqlFuncExternal> ExpMenthods() 
        {

            // 添加Sqlsugar自定义函数
            var expMethods = new List<SqlFuncExternal>();
            expMethods.Add(new SqlFuncExternal()
            {
                UniqueMethodName = "DateDiff",
                MethodValue = (expInfo, dbType, expContext) =>
                {
                    if (dbType == SqlSugar.DbType.SqlServer)
                        return string.Format("DATEDIFF(day,{0},{1})", expInfo.Args[0].MemberName, expInfo.Args[1].MemberName);
                    else
                        throw new Exception("未实现");
                }
            });

            expMethods.Add(new SqlFuncExternal()
            {
                UniqueMethodName = "DateToString",
                MethodValue = (expInfo, dbType, expContext) =>
                {
                    if (dbType == SqlSugar.DbType.SqlServer)
                        ////可以加switch 实现不同格式，后续增加
                        return string.Format("REPLACE(CONVERT(varchar(100), {0}, 112)+CONVERT(varchar(100), {0}, 8),':','')", expInfo.Args[0].MemberName);
                    else
                        throw new Exception("未实现");
                }
            });

            return expMethods;
        }


        public static int DateDiff(DateTime? date1, DateTime? date2)
        {
            //这里不能写任何实现代码，需要在上面的配置中实现
            throw new NotSupportedException("Can only be used in expressions");
        }

        public static string DateToString(DateTime? date1)
        {
            //这里不能写任何实现代码，需要在上面的配置中实现
            throw new NotSupportedException("Can only be used in expressions");
        }

    }
}
