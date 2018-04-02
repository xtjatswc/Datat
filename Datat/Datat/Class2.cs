using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat
{
    public class Class2
    {
        FluentContext fluentContext = new FluentContext();

        public void Test()
        {
            IDbContext mysqlContext = fluentContext.getMysqlContext();
            IDbContext sqlServerContext = fluentContext.getSqlServerContext();

            DataTable tbl = sqlServerContext.Sql("select * from patientbasicinfo").QuerySingle<DataTable>();

            StringBuilder sb = new StringBuilder();
            sb.Append("Create table patientbasicinfo2 Select ");

            List<object> lstParams = new List<object>();
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                DataRow row = tbl.NewRow();
                switch (tbl.Columns[i].DataType.ToString())
                {
                    case "System.Int64":
                        row[i] = 0m;
                        break;
                    case "System.Single":
                        row[i] = 0;
                        break;
                    case "System.String":
                        row[i] = new byte[4000];
                        break;
                    case "System.DateTime":
                        row[i] = DateTime.Now;
                        break;
                    default:
                        row[i] = null;
                        break;
                }

                sb.AppendFormat(" @{0} {1},", i, tbl.Columns[i].ColumnName);
                lstParams.Add(row[i]);
            }

            string sql = "";
            sql = sb.ToString().TrimEnd(',') + ";";

            int productId = mysqlContext.Sql(sql).Parameters(lstParams.ToArray()).Execute();

            //复制表数据
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            sb2.Append("insert into patientbasicinfo2(");
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                sb2.AppendFormat("{0},", tbl.Columns[i].ColumnName);
                sb3.AppendFormat("@{0},", i);
            }
            sb2 = new StringBuilder(sb2.ToString().TrimEnd(','));
            sb2.Append(" )values(");
            sb2.Append(sb3.ToString().TrimEnd(','));
            sb2.Append("); ");

            sql = sb2.ToString();


            foreach (DataRow row in tbl.Rows)
            {
                List<object> lstParams2 = new List<object>();

                foreach (DataColumn dc in tbl.Columns)
                {
                    lstParams2.Add(row[dc]);
                }
                int ret = mysqlContext.Sql(sql).Parameters(lstParams2.ToArray()).Execute();

            }

        }
    }
}
