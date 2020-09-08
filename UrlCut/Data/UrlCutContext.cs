using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlCut.Models;

namespace UrlCut.Data
{
    public class UrlCutContext : DbContext
    {
        public UrlCutContext (DbContextOptions<UrlCutContext> options)
            : base(options)
        {
        }

        public DbSet<UrlCut.Models.URL> URL { get; set; }
    }
}
