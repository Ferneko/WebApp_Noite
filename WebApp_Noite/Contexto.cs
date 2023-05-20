using Microsoft.EntityFrameworkCore;
using WebApp_Noite.Tabelas;
namespace WebApp_Noite
{
    public class Contexto : DbContext
    {
        public Contexto (DbContextOptions<Contexto> opt)
            :base(opt) { }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
    }
}
