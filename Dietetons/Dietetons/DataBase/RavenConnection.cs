using Dietetons.Utils;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.DataBase
{
    public class RavenConnection : IDatabaseConnection
    {
        public const string DataBaseName = "Diet";
        private const string LocalDbAddress = @"http://localhost:8080";
        private static DocumentStore localDocumentStore;
        public static DocumentStore LocalDocumentStore
        { 
            get
            {
                if (null == localDocumentStore)
                    localDocumentStore = new DocumentStore 
                    { 
                        Urls = LocalDbAddress.AsArray(),
                        Database = DataBaseName
                    };
                return localDocumentStore;
            } 
        }

        static RavenConnection()
        {
            LocalDocumentStore.Initialize();
        }
    }
}
