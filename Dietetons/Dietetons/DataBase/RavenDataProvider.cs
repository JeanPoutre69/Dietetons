using Dietetons.Utils;
using Raven.Client.Documents.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.DataBase
{
    public class RavenDataProvider : IDataBaseDataProvider
    {
        public RavenDataProvider()
        {
            new RavenConnection();
        }

        #region Adding
        public bool AddClient(Client client)
        {
            return Add(client);
        }

        public bool AddIngredient(Ingredient ingredient)
        {
            return Add(ingredient);
        }

        private bool Add<T>(T objToAdd)
        {
            try
            {
                using (var ravenSession = RavenConnection.LocalDocumentStore.OpenSession(RavenConnection.DataBaseName))
                {
                    ravenSession.Store(objToAdd);
                    ravenSession.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Getting
        public IList<Client> GetAllClients()
        {
            return GetAll<Client>();
        }

        public IList<Ingredient> GetAllIngredients()
        {
            return GetAll<Ingredient>();
        }

        private IList<T> GetAll<T>()
        {
            try
            {
                List<T> objectsToReturn;
                using (var session = RavenConnection.LocalDocumentStore.OpenSession(RavenConnection.DataBaseName))
                {
                    objectsToReturn =  session.Query<T>().ToList();
                }
                return objectsToReturn;
            } catch (Exception ex)
            {
                return null;
            }
        }

        public void GetMetaDataFor(string id)
        {
            try
            {
                using (var session = RavenConnection.LocalDocumentStore.OpenSession(RavenConnection.DataBaseName))
                {
                    var command = new GetDocumentsCommand(id, null, true);
                    session.Advanced.RequestExecutor.Execute(command, session.Advanced.Context);
                    var result = command.Result.Results.FirstOrDefault();
                    var json = result?.ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Updating
        public bool UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }

        public bool UpdateIngredient(Client client)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
