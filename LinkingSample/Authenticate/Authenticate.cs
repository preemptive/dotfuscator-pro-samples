using System;
using DatabaseAccess;

namespace Authenticator
{
    public class Authenticate
    {
        public static bool AuthenticateLogin(string username, string password)
        {
            Tuple<string, string> creds = new Tuple<string, string>(username, password);

            Query query = new Query();

            if (query.UserExists(creds))
                return true;
            else
                return false;
        }
    }
}