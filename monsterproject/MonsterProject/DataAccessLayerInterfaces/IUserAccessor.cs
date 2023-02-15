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
        User SelectUserByUserName(string username);
        List<string> SelectRolesByUserID(int userID);
        int UpdatePasswordHash(int userID, string passwordHash, string oldPasswordHash);

    }
}
