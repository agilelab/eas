using System;
using System.Collections.Generic;
using System.Text;

using EAS.Data.Access;

namespace fyiReporting.RdlDesign
{
    class DbHelper
    {
        public static IDbProvider GetDbProvider(string DataProvider, string connectString)
        {
            IDbProvider dbProvider = null;
            System.Type T = null;

            if (DataProvider == "MSSqlServer")
            {
                T = System.Type.GetType("EAS.Data.Access.SqlClientProvider,EAS.Data");
            }
            else if (DataProvider == "OleDBSupported")
            {
                T = System.Type.GetType("EAS.Data.Access.OleDbProvider,EAS.Data");
            }
            else if (DataProvider == "ODBC")
            {
                T = System.Type.GetType("EAS.Data.Access.OdbcProvider,EAS.Data");
            }
            else if (DataProvider == "Oracle")
            {
                if (connectString.ToUpper().IndexOf("MSDAORA") > -1)
                    T = System.Type.GetType("EAS.Data.Access.OleDbProvider,EAS.Data");
                else
                    T = System.Type.GetType("EAS.Data.Access.OracleProvider,EAS.Data.Provider");
            }

            dbProvider = (IDbProvider)System.Activator.CreateInstance(T);
            dbProvider.ConnectionString = connectString;
            return dbProvider;
        }
    }
}
