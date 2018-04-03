
using Datat.DbTypes;
using Datat.Trans;
using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Datat
{
    class Program
    {
        static void Main(string[] args)
        {

            List<DbParam> lstParams = DBCtor.GetDBParams();
            foreach (DbParam dbParam in lstParams)
            {
                Transmitters dataCopy = new Transmitters(dbParam);
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
