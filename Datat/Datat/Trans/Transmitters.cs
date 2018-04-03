using Datat.DbTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat
{
    public class Transmitters
    {
        public AbsDBSource sourceDatabase;
        public AbsDBTarget targetDatabase;

        private DataTable tbl;

        public Transmitters(DbParam dbParam)
        {
            this.sourceDatabase = DBCtor.getDBSource(dbParam);
            this.targetDatabase = DBCtor.getDBTarget(dbParam);

            tbl = sourceDatabase.GetSourceTable();
        }

        public void CopyTableStructure()
        {
            try
            {
                List<object> lstParams;
                string sql;
                targetDatabase.GetCreateTableSql(tbl, out lstParams, out sql);

                int ret = targetDatabase.GetDbContext().Sql(sql).Parameters(lstParams.ToArray()).Execute();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public void CopyTableData()
        {
            targetDatabase.CopyTableData(tbl);
        }

    }
}
