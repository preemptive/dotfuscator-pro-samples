using System;

namespace DatabaseAccess
{
    internal class UserList
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string> { { "admin", "admin123" } };

        public static bool CheckUserInList(Tuple<string, string> user)
        {
            if (_users.ContainsKey(user.Item1) && _users[user.Item1].Equals(user.Item2))
                return true;
            else 
                return false;
        }
    }

    public class Query
    {
        public bool UserExists(Tuple<string, string> creds)
        {
            if (UserList.CheckUserInList(creds))
                return true;
            else
                return false;
        }
    }
}