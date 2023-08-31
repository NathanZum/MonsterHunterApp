using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IUserAccessor
    {
        int AuthenticateUserWithUserNameAndPasswordHash(string username, string passwordHash);
        WPFUser SelectUserByUserName(string username);
        List<string> SelectRolesByUserID(int userID);
        int UpdatePasswordHash(int userID, string passwordHash, string oldPasswordHash);
        List<string> SelectAllRoles();
        int InsertUser(string username);
        int DeleteUserRole(int user_id, string role);
        int InsertUserRole(int user_id, string role);

    }
}
