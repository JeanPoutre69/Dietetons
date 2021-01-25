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

        public LocalDBDataManager()
        {
        }

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
            Action<SqlConnection, SqlTransaction> objectAdding = (connection, transaction) =>
            {
                var propertiesToHandle = objectToInsert.GetType().GetProperties()
                    .Where(x => x.CustomAttributes.Where(y => y.GetType() != typeof(NotToPickAttribute)).Count() == 0);

                // Forging the base of the request
                string basicRequestNotCompleted = BasicInsertRequest.Replace(ValuesInRequestStr, GetMultiValuesParameterStr(propertiesToHandle.Count()));
                string requestNoColumnNames = basicRequestNotCompleted.Replace(TableNameInRequestStr, ClientTableName);
                string request = requestNoColumnNames.Replace(TableColumnNamesInRequestStr, 
                    "(" + propertiesToHandle.Select(x => x.Name).Stringify(", ") + ")");
                
                var sqlCommand = new SqlCommand(request, connection, transaction);
                // Handling values
                int propertyCount = 1;
                foreach (var property in propertiesToHandle)
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
            if (propertyType == typeof(string))
            {
                return SqlDbType.NVarChar;
            } 
            else if (propertyType == typeof(int) || propertyType == typeof(int?))
            {
                return SqlDbType.Int;
            }
            else if (propertyType == typeof(double) || propertyType == typeof(double?))
            {
                return SqlDbType.Float;
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                return SqlDbType.Int;
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
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

        private bool SafeDBAction(Action<SqlConnection, SqlTransaction> action)
        {
            SqlTransaction transaction = null;
            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(LocalDatabaseConnection.ConnectionStr);
                connection.Open();
                transaction = connection.BeginTransaction();
                action(connection, transaction);
                transaction.Commit();
                return true;
            } catch (Exception ex)
            {
                transaction?.Rollback();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
