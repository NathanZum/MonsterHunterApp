using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayer;
using DataAccessLayerInterfaces;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor = null;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor ua)
        {
            _userAccessor = ua;
        }

        public bool AddUser(string username)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.InsertUser(username));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool AddUserRole(int user_id, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.InsertUserRole(user_id, role));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool FindUser(string username)
        {
            try
            {
                return _userAccessor.SelectUserByUserName(username) != null;
            }
            catch (ApplicationException ax)
            {
                if (ax.Message == "User not found.")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public string HashSha256(string source)
        {
            string result = "";

            // check for missing input
            if (source == null || source == "")
            {
                throw new ArgumentNullException("Missing Input");
            }

            // create a byte array
            byte[] data;
            // create a .NET hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                // hash the input
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            // create output with a stringbuilder object
            var s = new StringBuilder();
            // loop through the hashed output making characters from the values in the byte array
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            // convert the string builder into a string
            result = s.ToString();
            result = result.ToLower();

            return result;
        }

        public WPFUser LoginUser(string username, string password)
        {
            WPFUser user = null;

            try
            {
                password = HashSha256(password);
                if (1 == _userAccessor.AuthenticateUserWithUserNameAndPasswordHash(username, password))
                {
                    user = _userAccessor.SelectUserByUserName(username);
                    user.Roles = _userAccessor.SelectRolesByUserID(user.UserID);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Bad Username or Password", ex);
            }

            return user;
        }

        public bool RemoveUserRole(int user_id, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.DeleteUserRole(user_id, role));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool ResetPassword(WPFUser user, string username, string password, string oldpassword)
        {
            bool success = false;
            password = HashSha256(password);
            oldpassword = HashSha256(oldpassword);

            if (user.UserName != username)
            {
                success = false;
            }
            else if (1 == _userAccessor.UpdatePasswordHash(user.UserID, password, oldpassword))
            {
                success = true;
            }

            return success;
        }

        public bool ResetPassword(int userId, string password, string oldpassword)
        {
            bool success = false;
            password = HashSha256(password);
            oldpassword = HashSha256(oldpassword);

            if (1 == _userAccessor.UpdatePasswordHash(userId, password, oldpassword))
            {
                success = true;
            }

            return success;
        }

        public List<string> RetrieveEmployeeRoles()
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found", ex);
            }

            return roles;
        }

        public int RetrieveUserIDFromUsername(string username)
        {
            try
            {
                return _userAccessor.SelectUserByUserName(username).UserID;
            }
            catch ( Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }
    }
}
