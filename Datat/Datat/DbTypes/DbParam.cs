using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat.DbTypes
{
    public class DbParam
    {
        public string SourceConnName { get; set; }
        public string InputSql { get; set; }
        public string TargetConnName { get; set; }
        public string TargetTblName { get; set; }
    }
}
