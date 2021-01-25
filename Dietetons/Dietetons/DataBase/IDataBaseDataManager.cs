using Dietetons.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.DataBase
{
    public interface IDataBaseDataManager
    {
        #region Clients
        IList<Client> GetAllClients();
        bool UpdateClient(Client client);
        bool AddClient(Client client);
        #endregion

        #region Ingredients
        IList<Ingredient> GetAllIngredients();
        bool UpdateIngredient(Ingredient ingredient);
        bool AddIngredient(Ingredient ingredient);
        #endregion
    }
}
