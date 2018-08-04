using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMEntity;

namespace PMRepository
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Cloth> Cloths { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
