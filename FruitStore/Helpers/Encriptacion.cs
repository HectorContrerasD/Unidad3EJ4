using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;

namespace FruitStore.Helpers
{
    public static class Encriptacion //static es para que no se necesite instanciacion para usarse, como console
    {
        public static string StringToSHA512(string e)
        {

            using (var sha512 = SHA512.Create())
            {

            var arreglo = Encoding.UTF8.GetBytes(e);
            var hash = sha512.ComputeHash(arreglo);
            return Convert.ToHexString(hash);
            }
        }
        public static string FileToSHA512(string ruta)
        {
            var sha512 = SHA512.Create();
            var arreglo = File.ReadAllBytes(ruta);
            var hash = sha512.ComputeHash(arreglo);
            return Convert.ToHexString(hash);
        }
    }
}
