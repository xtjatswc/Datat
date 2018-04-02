using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat
{
    public class Class1
    {
        FluentContext fluentContext = new FluentContext();

        public void Test()
        {
            IDbContext mysqlContext = fluentContext.getMysqlContext();
            IDbContext sqlServerContext = fluentContext.getSqlServerContext();

            DataTable tbl = mysqlContext.Sql("select * from patientbasicinfo").QuerySingle<DataTable>();

            StringBuilder sb = new StringBuilder();
            sb.Append("select ");

            List<object> lstParams = new List<object>();
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                DataRow row = tbl.NewRow();
                object param = null; 
                switch (tbl.Columns[i].DataType.ToString())
                {
                    case "System.Int64":
                        row[i] = 0;
                        break;
                    case "System.Single":
                        row[i] = 0;
                        break;
                    case "System.String":
                        row[i] = null;
                        break;
                    case "System.DateTime":
                        row[i] = DateTime.Now;
                        break;
                    default:

                        break;
                }

                sb.AppendFormat(" @{0} {1},", i, tbl.Columns[i].ColumnName);
                lstParams.Add(row[i]);
            }

            string sql = "";
            sql = sb.ToString().TrimEnd(',') + " into patientbasicinfo;";

            int productId = sqlServerContext.Sql(sql).Parameters(lstParams.ToArray()).Execute();

            //复制表数据
            foreach (DataRow row in tbl.Rows)
            {

            }
        }
    }
}
