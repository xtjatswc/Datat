using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat
{
    public class DataCopy
    {
        public AbsDataBase sourceDatabase;
        public AbsDataBase targetDatabase;

        public DataCopy(AbsDataBase sourceDatabase, AbsDataBase targetDatabase)
        {
            this.sourceDatabase = sourceDatabase;
            this.targetDatabase = targetDatabase;
        }

        public void CopyTable()
        {
            DataTable tbl = sourceDatabase.GetSourceTable();
            List<object> lstParams;
            string sql;
            targetDatabase.GetCreateTableSql(tbl, out lstParams, out sql);

            int ret = targetDatabase.GetDbContext().Sql(sql).Parameters(lstParams.ToArray()).Execute();
        }

  

        public void CopyData()
        {
            DataTable tbl = sourceDatabase.GetSourceTable();

            //复制表数据
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            sb2.AppendFormat("insert into {0}(", targetDatabase.param.TargetTableName);
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                sb2.AppendFormat("{0},", tbl.Columns[i].ColumnName);
                sb3.AppendFormat("@{0},", i);
            }
            sb2 = new StringBuilder(sb2.ToString().TrimEnd(','));
            sb2.Append(" )values(");
            sb2.Append(sb3.ToString().TrimEnd(','));
            sb2.Append("); ");

            string sql = sb2.ToString();


            foreach (DataRow row in tbl.Rows)
            {
                List<object> lstParams2 = new List<object>();

                foreach (DataColumn dc in tbl.Columns)
                {
                    lstParams2.Add(row[dc]);
                }
                int ret = targetDatabase.GetDbContext().Sql(sql).Parameters(lstParams2.ToArray()).Execute();

            }
        }

    }
}
