using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APID.Helpers
{
    public class HashUtils
    {


         public static void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


         public static bool VerifyPasswordHash(string password,byte[] passwordHash,  byte[] passwordSalt){
          using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != passwordHash[i]) return false;
                }               
            }
             return true;
        }        
        
    }
}