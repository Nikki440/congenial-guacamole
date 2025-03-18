using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    using Bogus;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Identity.Client;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Runtime.CompilerServices;

    namespace Dierentuin_eindopdracht.Models
    {
        public class Database : DbContext
        {
            public Database(DbContextOptions<Database> options) : base(options)
            {
            }

            // DbSets
            public DbSet<Animal> Animals { get; set; }
            public DbSet<Enclosure> Enclosures { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Zoo> Zoos { get; set; }
        }
    }
}