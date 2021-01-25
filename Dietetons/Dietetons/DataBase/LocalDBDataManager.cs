using Dietetons.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.DataBase
{
    public class LocalDBDataManager : IDataBaseDataManager
    {
        private const string BasicInsertRequest = "INSERT INTO @TableName @TableColumnNames VALUES @Values";
        private const string TableNameInRequestStr = "@TableName";
        private const string TableColumnNamesInRequestStr = "@TableColumnNames";
        private const string ValuesInRequestStr = "@Values";
        private const string ClientTableName = "Clients";

        public bool AddClient(Client client)
        {
            return Insert<Client>(ClientTableName, client);
        }

        public bool AddIngredient(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public IList<Client> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public IList<Ingredient> GetAllIngredients()
        {
            throw new NotImplementedException();
        }

        public bool UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }

        public bool UpdateIngredient(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not efficient but lazy way to insert an object into a DB. Properties must match the column names and types of a table in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="objectToInsert"></param>
        /// <returns></returns>
        private bool Insert<T>(string tableName, T objectToInsert)
        {
            Action objectAdding = () =>
            {
                var properties = objectToInsert.GetType().GetProperties();
                string request = BasicInsertRequest.Replace(ValuesInRequestStr, GetMultiValuesParameterStr(properties.Count()));
                var sqlCommand = new SqlCommand(request, LocalDatabaseConnection.Connection);

                // Handling table name
                sqlCommand.Parameters.AddWithValue(TableNameInRequestStr, tableName);

                // Handling column names
                var propertiesNames = properties.Where(x => x.GetCustomAttributes(typeof(NotToPickAttribute), false).Count() == 0).Select(x => x.Name);
                sqlCommand.Parameters.AddWithValue(TableColumnNamesInRequestStr, "(" + propertiesNames.Stringify(", ") + ")");

                // Handling values
                int propertyCount = 1;
                foreach (var property in properties)
                {
                    var sqlParameter = sqlCommand.Parameters.Add(ValuesInRequestStr + propertyCount, GetSqlType(property.PropertyType));
                    sqlParameter.Value = objectToInsert.GetPropertyValue(property.Name);
                    propertyCount += 1;
                }

                // Inserting
                sqlCommand.ExecuteNonQuery();
            };
            return SafeDBAction(objectAdding);
        }

        private SqlDbType GetSqlType(Type propertyType)
        {
            if (propertyType.IsInstanceOfType(typeof(string)) || propertyType.IsSubclassOf(typeof(string)))
            {
                return SqlDbType.NVarChar;
            } 
            else if (propertyType.IsInstanceOfType(typeof(int)) || propertyType.IsSubclassOf(typeof(int)))
            {
                return SqlDbType.Int;
            }
            else if (propertyType.IsInstanceOfType(typeof(double)) || propertyType.IsSubclassOf(typeof(double)))
            {
                return SqlDbType.Float;
            }
            else if (propertyType.IsInstanceOfType(typeof(bool)) || propertyType.IsSubclassOf(typeof(bool)))
            {
                return SqlDbType.Int;
            }
            else if (propertyType.IsInstanceOfType(typeof(DateTime)) || propertyType.IsSubclassOf(typeof(DateTime)))
            {
                return SqlDbType.DateTime;
            }
            else
            {
                throw new Exception("Type not handled");
            }
        }

        private string GetMultiValuesParameterStr(int numberOfValues)
        {
            var strToReturn = "(";
            for (int i = 1; i <= numberOfValues - 1; i++)
            {
                strToReturn += ValuesInRequestStr + i + ", ";
            }
            strToReturn += ValuesInRequestStr + numberOfValues + ")";
            return strToReturn;
        }

        private bool SafeDBAction(Action action)
        {
            var transaction = LocalDatabaseConnection.Connection.BeginTransaction();
            try
            {
                LocalDatabaseConnection.Connection.Open();
                action();
                transaction.Commit();
                return true;
            } catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                LocalDatabaseConnection.Connection.Close();
            }
        }
    }
}
