using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.DataBase
{
    public static class LocalDatabaseConnection
    {
        public static SqlConnection Connection { get { return new SqlConnection(
            System.Configuration.ConfigurationManager.ConnectionStrings["LocalDBConnection"].ConnectionString); } }
    }
}
