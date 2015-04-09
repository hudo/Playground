using System.IO;

namespace CookieConsent.Service
{
    public class FileShim : IFileShim
    {
        private readonly string _wwwroot;

        public FileShim(string wwwroot)
        {
            _wwwroot = wwwroot;
        }

        public bool Exists(string location)
        {
            return File.Exists(Path.Combine(_wwwroot, location));
        }

        public string ReadAllText(string location)
        {
            return File.ReadAllText(_wwwroot + location);
        }
    }
}