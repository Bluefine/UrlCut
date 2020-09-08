using UrlCut.Models;

namespace UrlCut.Interfaces
{
    public interface IShortener
    {
        public string GenerateUniqueToken();
        public bool AlreadyExists(string link);
        public bool Add(string link, string token);
    }
}
