using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat.DbTypes
{
    public class SqlServerTarget : AbsDBTarget
    {
        public SqlServerTarget(DbParam param)
        {
            this.param = param;
        }

        public override IDbContext GetDbContext()
        {
            return new DbContext().ConnectionStringName(param.TargetConnName,
        new SqlServerProvider());
        }

        public override void GetCreateTableSql(DataTable tbl, out List<object> lstParams, out string sql)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");

            lstParams = new List<object>();
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                DataRow row = tbl.NewRow();
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
                        row[i] = null;
                        break;
                }

                sb.AppendFormat(" @{0} {1},", i, tbl.Columns[i].ColumnName);
                lstParams.Add(row[i]);
            }

            sql = "";
            sql = sb.ToString().TrimEnd(',') + string.Format(" into {0};TRUNCATE TABLE {0};", param.TargetTblName);
        }
    }
}
