using FluentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat.Trans
{
    public class FluentDBContext
    {
        public static IDbContext GetSqliteContext(string connName)
        {
            return new DbContext().ConnectionStringName(connName,
                    new SqliteProvider());
        }
    }
}
