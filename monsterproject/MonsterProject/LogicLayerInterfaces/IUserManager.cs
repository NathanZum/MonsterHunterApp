using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        WPFUser LoginUser(string username, string password);
        string HashSha256(string source);
        bool ResetPassword(WPFUser user, string username, string password, string oldpassword);
        bool ResetPassword(int userId, string password, string oldpassword);
        List<string> RetrieveEmployeeRoles();
        bool FindUser(string username);
        int RetrieveUserIDFromUsername(string username);
        bool AddUser(string username);
        bool RemoveUserRole(int user_id, string role);
        bool AddUserRole(int user_id, string role);
    }
}
