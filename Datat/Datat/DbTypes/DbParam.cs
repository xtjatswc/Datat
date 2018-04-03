using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat.DbTypes
{
    public class DbParam
    {
        public int ID { get; set; }
        public DBType SourceDBType { get; set; }
        public string SourceConnName { get; set; }
        public string InputSql { get; set; }
        public DBType TargetDBType { get; set; }
        public string TargetConnName { get; set; }
        public string TargetTblName { get; set; }
        public string PrimaryKey { get; set; }
        public int Enabled { get; set; }

    }

    public enum DBType
    {
        mysql = 0,
        sqlserver = 1,
        sqlite = 3,
        oracle = 4
    }
}
