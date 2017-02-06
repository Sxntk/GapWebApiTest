namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class ArticleAccess
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? TotalInShelf { get; set; }

        public decimal? TotalInVault { get; set; }

        public ArticleAccess()
        {

        }

        public ArticleAccess(string name, string description, decimal price, decimal? totalinshelf, decimal? totalinvault, Guid storeid)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.TotalInShelf = totalinshelf;
            this.TotalInVault = totalinvault;
            this.StoreId = storeid;
        }

        public static IEnumerable<ArticleAccess> GetAllArticles()
        {
            try
            {
                List<ArticleAccess> lista = new List<ArticleAccess>();

                string queryString = "SELECT Articles.[id], Articles.[name], description, price, total_in_shelf, total_in_vault, Stores.name, Stores.id " +
                    "FROM [dbo].[tablearticles] as Articles inner join [tablestores] as Stores on Articles.store_id = Stores.id;";
                using (SqlConnection connection = new SqlConnection(Common.Connection))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            ArticleAccess article = new ArticleAccess();
                            article.Id = (Guid)reader[0];
                            article.Name = (string)reader[1];
                            article.Description = reader[2] as string;
                            article.Price = (decimal)reader[3];
                            article.TotalInShelf = reader[4] as decimal?;
                            article.TotalInVault = reader[5] as decimal?;
                            article.StoreName = (string)reader[6];
                            article.StoreId = (Guid)reader[7];
                            lista.Add(article);
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

        public static IEnumerable<ArticleAccess> GetArticlesByStoreId(Guid id)
        {
            try
            {
                List<ArticleAccess> lista = new List<ArticleAccess>();

                string queryString = "SELECT Articles.[id], Articles.[name], description, price, total_in_shelf, total_in_vault, Stores.name, Stores.id " +
                    "FROM [dbo].[tablearticles] as Articles "+
                    "inner join [tablestores] as Stores on Articles.store_id = Stores.id where Articles.[store_id] = '" + id + "';";
                using (SqlConnection connection = new SqlConnection(Common.Connection))
                {
                    using(SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                ArticleAccess article = new ArticleAccess();
                                article.Id = (Guid)reader[0];
                                article.Name = (string)reader[1];
                                article.Description = reader[2] as string;
                                article.Price = (decimal)reader[3];
                                article.TotalInShelf = reader[4] as decimal?;
                                article.TotalInVault = reader[5] as decimal?;
                                article.StoreName = (string)reader[6];
                                article.StoreId = (Guid)reader[7];
                                lista.Add(article);
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
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

        public static bool CreateArticle(ArticleAccess article)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Common.Connection))
                {
                    using (SqlCommand command = new SqlCommand("SPCreateArticle", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = article.Name;
                        command.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = article.Description;
                        command.Parameters.Add("@PRICE", SqlDbType.Money).Value = article.Price;
                        command.Parameters.Add("@TOTALINSHELF", SqlDbType.Decimal).Value = article.TotalInShelf;
                        command.Parameters.Add("@TOTALINVAULT", SqlDbType.Decimal).Value = article.TotalInVault;
                        command.Parameters.Add("@STOREID", SqlDbType.UniqueIdentifier).Value = article.StoreId;

                        connection.Open();
                        object resultado = command.ExecuteScalar();
                        article.Id = (Guid)resultado;
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