using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public int AuthenticateUserWithUserNameAndPasswordHash(string username, string passwordHash)
        {
            int result = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_authenticate_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@password_hash", SqlDbType.NVarChar, 100);
            cmd.Parameters["@username"].Value = username;
            cmd.Parameters["@password_hash"].Value = passwordHash;
            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            { 
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<string>();

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_roles_by_appuser_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@appuser_id", SqlDbType.Int);

            // value
            cmd.Parameters["@appuser_id"].Value = userID;

            try
            {
                // open connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                //process results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close connection
                conn.Close();
            }


            return roles;
        }

        public User SelectUserByUserName(string username)
        {
            User user = null;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_user_by_username";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 30);

            //value
            cmd.Parameters["@username"].Value = username;

            try
            {
                // open
                conn.Open();

                // execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();

                user = new User();

                if (reader.HasRows)
                {
                    // most of the time there will be a while loop here
                    // we dont need it for this one

                    reader.Read();
                    // [EmployeeID], [GivenName], [FamilyName], [Phone], [Email], [Active]

                    user.UserID = reader.GetInt32(0);
                    user.UserName = reader.GetString(1);
                    user.Active = reader.GetBoolean(2);
                }
                // close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //close
                conn.Close();
            }

            return user;
        }

        public int UpdatePasswordHash(int userID, string passwordHash, string oldPasswordHash)
        {
            int rows = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_password_hash";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@appuser_id", userID);
            cmd.Parameters.AddWithValue("@password_hash", passwordHash);
            cmd.Parameters.AddWithValue("@OldPasswordHash", oldPasswordHash);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
