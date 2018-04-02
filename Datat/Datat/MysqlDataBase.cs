using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace Datat
{
    public class MysqlDataBase : AbsDataBase
    {
        Param param;

        public MysqlDataBase(Param param)
        {
            this.param = param;
        }

        public override IDbContext GetDbContext()
        {
            return new DbContext().ConnectionStringName(param.ConnStr,
                    new MySqlProvider());
        }

        public override DataTable GetSourceTable()
        {
            return GetDbContext().Sql(param.SelectSql).QuerySingle<DataTable>();

        }

        public override void GetCreateTableSql(DataTable tbl, out List<object> lstParams, out string sql)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Create table {0} Select ", param.TargetTableName);

            lstParams = new List<object>();
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

            sql = "";
            sql = sb.ToString().TrimEnd(',') + ";";
        }
    }
}
