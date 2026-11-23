using System.Security.Cryptography;
using System.Text;
namespace lib_aplicaciones.nucleo
{
    public static class Cifrado
    {
        // Convierte un texto en su resumen SHA-256 (64 caracteres). No se puede volver al texto original.
        public static string Sha256(string texto)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes);
        }
    }
}
