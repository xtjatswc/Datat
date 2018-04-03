using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat.DbTypes
{
    public abstract class AbsDBTarget
    {
        public DbParam param;

        public abstract IDbContext GetDbContext();
        public abstract void GetCreateTableSql(DataTable tbl, out List<object> lstParams, out string sql);

        /// <summary>
        /// 复制表数据
        /// </summary>
        /// <param name="absDBSource"></param>
        public void CopyTableData(AbsDBSource absDBSource)
        {
            DataTable tbl = absDBSource.GetSourceTable();

            string insertSql = GetInsertSql(tbl);
            string udpateSql = GetUpdateSql(tbl);

            foreach (DataRow row in tbl.Rows)
            {
                List<object> lstParams2 = new List<object>();

                foreach (DataColumn dc in tbl.Columns)
                {
                    lstParams2.Add(row[dc]);
                }
                try
                {
                    int ret = GetDbContext().Sql(insertSql).Parameters(lstParams2.ToArray()).Execute();
                    Console.WriteLine("insert_" + row[param.PrimaryKey]);
                }
                catch (Exception ex)
                {
                    lstParams2.Add(row[param.PrimaryKey]);
                    int ret = GetDbContext().Sql(udpateSql).Parameters(lstParams2.ToArray()).Execute();
                    Console.WriteLine("update_" + row[param.PrimaryKey]);
                }
            }
        }

        private string GetInsertSql(DataTable tbl)
        {
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            sb2.AppendFormat("insert into {0}(", param.TargetTblName);
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
            return sql;
        }

        private string GetUpdateSql(DataTable tbl)
        {
            StringBuilder sb2 = new StringBuilder();

            sb2.AppendFormat("update {0} set ", param.TargetTblName);
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                sb2.AppendFormat("{0}=@{1},", tbl.Columns[i].ColumnName, i);
            }
            sb2 = new StringBuilder(sb2.ToString().TrimEnd(','));
            sb2.Append(" where " + param.PrimaryKey + "=@" + tbl.Columns.Count);

            string sql = sb2.ToString();
            return sql;
        }

    }

}
