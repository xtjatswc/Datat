using Datat.Trans;
using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat.DbTypes
{
    public class DBCtor
    {
        public static List<DbParam> GetDBParams()
        {
            IDbContext sqliteContext = FluentDBContext.GetSqliteContext("SqliteConnStr");
            List<DbParam> lstParams = sqliteContext.Sql("select * from TranTask where Enabled = 1").QueryMany<DbParam>();

            return lstParams;
        }

        public static DbParam GetDBParam(string id)
        {
            IDbContext sqliteContext = FluentDBContext.GetSqliteContext("SqliteConnStr");
            DbParam dbParam = sqliteContext.Sql("select * from TranTask where ID = " + id).QuerySingle<DbParam>();

            return dbParam;
        }


        public static AbsDBSource GetDBSource(DbParam dbParam)
        {
            AbsDBSource absDBSource = null;
            switch (dbParam.SourceDBType)
            {
                case DBType.mysql:
                    absDBSource = new MysqlSource(dbParam);
                    break;
                case DBType.sqlserver:
                    absDBSource = new SqlServerSource(dbParam);
                    break;
                default:
                    break;
            }
            return absDBSource;
        }

        public static AbsDBTarget GetDBTarget(DbParam dbParam)
        {
            AbsDBTarget absDBTarget = null;
            switch (dbParam.TargetDBType)
            {
                case DBType.mysql:
                    absDBTarget = new MysqlTarget(dbParam);
                    break;
                case DBType.sqlserver:
                    absDBTarget = new SqlServerTarget(dbParam);
                    break;
                default:
                    break;
            }
            return absDBTarget;
        }
    }
}
