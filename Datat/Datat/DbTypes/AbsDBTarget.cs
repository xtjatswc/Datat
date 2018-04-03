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
    }
}
