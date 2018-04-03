using Datat.DbTypes;
using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Datat
{
    public class Class1
    {
        public void Test()
        {
            DbParam dbParam = new DbParam();
            dbParam.SourceConnName = "MysqlConnStr";
            dbParam.InputSql = "select * from patientbasicinfo";
            dbParam.TargetConnName = "SqlServerConnStr";
            dbParam.TargetTblName = "patientbasicinfo";
            dbParam.PrimaryKey = "PATIENT_DBKEY";

            Transmitters dataCopy = new Transmitters(new MysqlSource(dbParam), new SqlServerTarget(dbParam));
            try
            {
                dataCopy.CopyTableStructure();
                dataCopy.CopyTableData();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Thread.Sleep(10 * 1000);
            }

            //DbParam p2 = new DbParam();
            //p2.ConfigConnName = "SqlServerConnStr";
            //p2.InputSql = "select * from patientbasicinfo";
            //p2.TargetTblName = "";

            //DbParam p1 = new DbParam();
            //p1.ConfigConnName = "MysqlConnStr";
            //p1.InputSql = "";
            //p1.TargetTblName = "patientbasicinfo2";

            //Transmitters dataCopy = new Transmitters(new SqlServerDataBase(p2), new MysqlDataBase(p1));
            //dataCopy.CopyTableStructure();
            //dataCopy.CopyTableData();




        }
    }
}
