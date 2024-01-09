using System;
using System.Text;
namespace ApplicationCommon
{
    public class GeneratePassword
    {
        protected  static string GenerateNewPassword()
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int passwordLength = 10;
            StringBuilder res = new();
            Random rnd = new();
            while (0 < passwordLength)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
                passwordLength--;
            }
            return res.ToString();
        }
    }
}
