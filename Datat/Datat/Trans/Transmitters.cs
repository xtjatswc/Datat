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

        public Transmitters(AbsDBSource sourceDatabase, AbsDBTarget targetDatabase)
        {
            this.sourceDatabase = sourceDatabase;
            this.targetDatabase = targetDatabase;
        }

        public void CopyTableStructure()
        {
            try
            {
                DataTable tbl = sourceDatabase.GetSourceTable();
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
            targetDatabase.CopyTableData(sourceDatabase);
        }

    }
}
