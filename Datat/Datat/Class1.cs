using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datat
{
    public class Class1
    {
        public void Test()
        {
            Param p1 = new Param();
            p1.ConnStr = "MysqlConnStr";
            p1.SelectSql = "select * from patientbasicinfo";
            p1.TargetTableName = "";

            Param p2 = new Param();
            p2.ConnStr = "SqlServerConnStr";
            p2.SelectSql = "";
            p2.TargetTableName = "patientbasicinfo";

            DataCopy dataCopy = new DataCopy(new MysqlDataBase(p1), new SqlServerDataBase(p2));
            dataCopy.CopyTable();
            dataCopy.CopyData();






        }
    }
}
