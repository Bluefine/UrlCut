using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UrlCut.Data;
using UrlCut.Interfaces;
using UrlCut.Models;

namespace UrlCut.Services
{
    public class Shortener : IShortener
    {
        private string letters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
        private UrlCutContext _context;

        public Shortener(UrlCutContext context)
        {
            _context = context;
        }

        public bool Add(string link, string token)
        {
            var guid = new Guid();
            var url = new URL {Created = DateTime.Now, Guid = guid.ToString(), Link = link, Token = token};
            _context.URL.Add(url);
            return _context.SaveChanges() == 1;
        }

        public bool AlreadyExists(string link)
        {
            return _context.URL.ToList().Exists(x => x.Link == link);
        }

        private string GenerateToken()
        {
            var token = string.Empty;
            var rand = new Random();
            for (var i = 0; i < 5; i++)
            {
                var rndInt = rand.Next(0, letters.Length);
                token += letters[rndInt];
            }

            return token;
        }

        private bool TokenExists(string token)
        {
            return _context.URL.ToList().Exists(x => x.Token == token);
        }

        public string GenerateUniqueToken()
        {
            var token = GenerateToken();
            while (TokenExists(token))
            {
                token = GenerateToken();
            }

            return token;
        }
    }
}
