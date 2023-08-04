using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kadince_Todo_Ramanujam.Models;

namespace Kadince_Todo_Ramanujam.Data
{
    public class Kadince_Todo_RamanujamContext : DbContext
    {
        public Kadince_Todo_RamanujamContext (DbContextOptions<Kadince_Todo_RamanujamContext> options)
            : base(options)
        {
        }

        public DbSet<Kadince_Todo_Ramanujam.Models.TodoItem> TodoItem { get; set; } = default!;
    }
}
