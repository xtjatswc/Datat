
using Datat.DbTypes;
using Datat.Trans;
using FluentData;
using System;
using System.Data;
using System.Threading;

namespace Datat
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbContext sqliteContext = FluentDBContext.GetSqliteContext("SqliteConnStr");
            DataTable dt = sqliteContext.Sql("select * from TranTask where Enabled = 1").QuerySingle<DataTable>();

            foreach (DataRow row in dt.Rows)
            {
                DbParam dbParam = new DbParam();
                dbParam.SourceConnName = row["SourceConnName"].ToString();
                dbParam.InputSql = row["InputSql"].ToString();
                dbParam.TargetConnName = row["TargetConnName"].ToString();
                dbParam.TargetTblName = row["TargetTblName"].ToString();
                dbParam.PrimaryKey = row["PrimaryKey"].ToString();

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
            }
        }

    }
}
