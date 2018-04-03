using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace Datat.DbTypes
{
    public class SqlServerSource : AbsDBSource
    {
        public SqlServerSource(DbParam param)
        {
            this.param = param;
        }

        public override IDbContext GetDbContext()
        {
            return new DbContext().ConnectionStringName(param.SourceConnName,
        new SqlServerProvider());
        }

        public override DataTable GetSourceTable()
        {
            return GetDbContext().Sql(param.InputSql).QuerySingle<DataTable>();
        }

        
    }
}
