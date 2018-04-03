using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace Datat.DbTypes
{
    public class MysqlTarget : AbsDBTarget
    {
        public MysqlTarget(DbParam param)
        {
            this.param = param;
        }

        public override IDbContext GetDbContext()
        {
            return new DbContext().ConnectionStringName(param.TargetConnName,
                    new MySqlProvider());
        }

        public override void GetCreateTableSql(DataTable tbl, out List<object> lstParams, out string sql)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbModifyType = new StringBuilder(" ALTER TABLE " + param.TargetTblName + " ");
            sb.AppendFormat(" Create table {0} Select ", param.TargetTblName);

            lstParams = new List<object>();
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                DataRow row = tbl.NewRow();
                switch (tbl.Columns[i].DataType.ToString())
                {
                    case "System.Int64":
                        sbModifyType.Append("MODIFY COLUMN `" + tbl.Columns[i].ColumnName + "` bigint(0),");
                        break;
                    case "System.Single":
                        sbModifyType.Append("MODIFY COLUMN `" + tbl.Columns[i].ColumnName + "` int(0),");
                        break;
                    case "System.String":
                        sbModifyType.Append("MODIFY COLUMN `" + tbl.Columns[i].ColumnName + "` varchar(100),");
                        break;
                    case "System.DateTime":
                        sbModifyType.Append("MODIFY COLUMN `" + tbl.Columns[i].ColumnName + "` datetime,");
                        break;
                    default:
                        sbModifyType.Append("MODIFY COLUMN `" + tbl.Columns[i].ColumnName + "` varchar(100),");
                        break;
                }

                sb.AppendFormat(" @{0} {1},", i, tbl.Columns[i].ColumnName);
                lstParams.Add(row[i]);
            }

            sql = "";
            sql = sb.ToString().TrimEnd(',') + ";TRUNCATE table " + param.TargetTblName + ";" + sbModifyType.ToString().TrimEnd(',') + ";";
        }
    }
}
