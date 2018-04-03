using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat.DbTypes
{
    public abstract class AbsDBSource
    {
        public DbParam param;

        public abstract DataTable GetSourceTable();
        public abstract IDbContext GetDbContext();
    }
}
