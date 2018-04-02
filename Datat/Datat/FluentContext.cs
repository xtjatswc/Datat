using FluentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat
{
    public class FluentContext
    {
        public IDbContext getMysqlContext()
        {
            return new DbContext().ConnectionStringName("MysqlConnStr",
                    new MySqlProvider());
        }

        public IDbContext getSqlServerContext()
        {
            return new DbContext().ConnectionStringName("SqlServerConnStr",
                    new SqlServerProvider());
        }

    }
}
