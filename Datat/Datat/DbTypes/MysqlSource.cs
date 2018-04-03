using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace Datat.DbTypes
{
    public class MysqlSource : AbsDBSource
    {
        public MysqlSource(DbParam param)
        {
            this.param = param;
        }

        public override IDbContext GetDbContext()
        {
            return new DbContext().ConnectionStringName(param.SourceConnName,
                    new MySqlProvider());
        }

        public override DataTable GetSourceTable()
        {
            return GetDbContext().Sql(param.InputSql).QuerySingle<DataTable>();

        }

       
    }
}
