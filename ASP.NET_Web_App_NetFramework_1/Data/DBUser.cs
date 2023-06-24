using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ASP.NET_Web_App_NetFramework_1.Models;
using System.Data;
using System.Data.SqlClient;

namespace ASP.NET_Web_App_NetFramework_1.Data
{
    public class DBUser
    {
        private static string SQLString = "Server=(local); DataBase=db_AspNetWebAppNetFramework_1; Trusted_Connection=True; TrustServerCertificate=True;";

        // User Register Method
        public static bool Register(UserDTO user)
        {
            bool response = false;

            try
            {
                using (SqlConnection oconnection = new SqlConnection(SQLString))
                {
                    string query = "insert into Users(uName, uEmail, uPassword, uReset, uConfirmed, uToken)";
                    query += " values(@name, @email, @password, @reset, @confirmed, @token)";

                    SqlCommand cmd = new SqlCommand(query, oconnection);
                    cmd.Parameters.AddWithValue("@name", user.uName);
                    cmd.Parameters.AddWithValue("@email", user.uEmail);
                    cmd.Parameters.AddWithValue("@password", user.uPassword);
                    cmd.Parameters.AddWithValue("@reset", user.uReset);
                    cmd.Parameters.AddWithValue("@confirmed", user.uConfirmed);
                    cmd.Parameters.AddWithValue("@token", user.uToken);
                    cmd.CommandType = CommandType.Text;

                    oconnection.Open();

                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        response = true;
                    }
                }

                return response;

            }
            catch(Exception ex) {
                throw ex;
            }

        }

        // User Validate Method
        public static UserDTO Validate(string email, string password)
        {
            UserDTO user = null;

            try
            {
                using (SqlConnection oconnection = new SqlConnection(SQLString))
                {
                    string query = "select uName, uReset, uConfirmed from Users";
                    query += " where uEmail = @email and uPassword = @password";

                    SqlCommand cmd = new SqlCommand(query, oconnection);                    
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);                    
                    cmd.CommandType = CommandType.Text;

                    oconnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new UserDTO()
                            {
                                uName = dr["uName"].ToString(),
                                uReset = (bool)dr["uReset"],
                                uConfirmed = (bool)dr["uConfirmed"],
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;

        }

        // Get User By Email Method
        public static UserDTO GetUserByEmail(string email)
        {
            UserDTO user = null;

            try
            {
                using (SqlConnection oconnection = new SqlConnection(SQLString))
                {
                    string query = "select uName, uPassword, uReset, uConfirmed, uToken from Users";
                    query += " where uEmail = @email";

                    SqlCommand cmd = new SqlCommand(query, oconnection);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.CommandType = CommandType.Text;

                    oconnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new UserDTO()
                            {
                                uName = dr["uName"].ToString(),
                                uPassword = dr["uPassword"].ToString(),
                                uReset = (bool)dr["uReset"],
                                uConfirmed = (bool)dr["uConfirmed"],
                                uToken = dr["uToken"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;

        }

        // User Reset-Update Method
        public static bool ResetUpdate(int reset, string password, string token)
        {
            bool response = false;

            try
            {
                using (SqlConnection oconnection = new SqlConnection(SQLString))
                {
                    string query = @"update Users set " +
                        "uReset = @reset, " +
                        "uPassword = @password " +
                        "where uToken = @token";

                    SqlCommand cmd = new SqlCommand(query, oconnection);
                    cmd.Parameters.AddWithValue("@reset", reset);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    oconnection.Open();

                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        response = true;
                    }
                }

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // User Confirm Method
        public static bool Confirm(string token)
        {
            bool response = false;

            try
            {
                using (SqlConnection oconnection = new SqlConnection(SQLString))
                {
                    string query = @"update Users set " +
                        "uConfirmed = 1 " +
                        "where uToken = @token";

                    SqlCommand cmd = new SqlCommand(query, oconnection);                    
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    oconnection.Open();

                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        response = true;
                    }
                }

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}