namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class StoreAccess
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public StoreAccess()
        {

        }

        public StoreAccess(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }

        public static IEnumerable<StoreAccess> GetAllStores()
        {
            try
            {
                List<StoreAccess> lista = new List<StoreAccess>();

                string queryString = "SELECT TOP 1000 [id], [name], [address] FROM [dbSuperZapatos].[dbo].[tablestores]; ";
                using (SqlConnection connection = new SqlConnection(Common.Connection))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            StoreAccess store = new StoreAccess();
                            store.Id = (Guid)reader[0];
                            store.Name = (string)reader[1];
                            store.Address = reader[2] as string;
                            lista.Add(store);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                // Log the error, then throw it to web api layer
                throw;
            }
        }

        public static bool CreateStore(StoreAccess store)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Connection))
                {
                    using (SqlCommand command = new SqlCommand("SPCreateStore", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = store.Name;
                        command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = store.Address;

                        connection.Open();
                        object resultado = command.ExecuteScalar();
                        store.Id = (Guid)resultado;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
