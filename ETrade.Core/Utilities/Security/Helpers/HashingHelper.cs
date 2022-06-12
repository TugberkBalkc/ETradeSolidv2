using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Security.Helpers
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmacsha512 = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmacsha512.Key;
                passwordHash = hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmacsha512 = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(password));

                return CompareComputedAndOriginalHash(computedHash, passwordHash);
            }
        }

        private static bool CompareComputedAndOriginalHash(byte[] computedHash, byte[] originalHash)
        {
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != originalHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
