using System;
namespace hospital.Helpers
{
    public interface IHashPassword {
        string Encypt(string input);
    }
    public class Rsa : IHashPassword
    {
       
        public string Encypt(string input)
        {
            return "RSA algorithm not implemented";
        }
    }
}
