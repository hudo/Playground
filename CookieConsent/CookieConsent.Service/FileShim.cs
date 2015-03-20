using System.IO;

namespace CookieConsent.Service
{
    public class FileShim : IFileShim
    {
        public bool Exists(string location)
        {
            return File.Exists(location);
        }

        public string ReadAllText(string location)
        {
            return File.ReadAllText(location);
        }
    }
}