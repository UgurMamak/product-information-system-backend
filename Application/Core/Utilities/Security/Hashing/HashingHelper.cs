using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //Hash'lı şifre oluşturma ve Doğrulama işlemlerini gerçekleştirecek class

        //register işleminde kullanıcının girdiği parolayı hashleme işlemi gerçekleştirecek.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())//hashing algoritmamızı verdik
            {
                //burada ortaya çıkan değerler parametre olarak verilen değerlere atanmış oldu.(out byte[] passwordHash, out byte[] passwordSalt)
                passwordSalt = hmac.Key;//anahtarımız yani bize passwordsalt oluşturacak.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//
            }
        }

        //Girilen password ile DB'deki password ile karşılaştıracak.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//kullanıcının girdiği password hashlendi.

                //girilen hash ile Db'deki hash karşılaştırılıyor.
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
