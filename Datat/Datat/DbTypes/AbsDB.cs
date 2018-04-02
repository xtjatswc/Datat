using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat
{
    public abstract class AbsDB
    {
        public DbParam param;

        public abstract DataTable GetSourceTable();
        public abstract IDbContext GetDbContext();
        public abstract void GetCreateTableSql(DataTable tbl, out List<object> lstParams, out string sql);
    }
}
