using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Data
{
    public class DbConfiguration : DbMigrationsConfiguration<DataContext>
    {
        private Random _random;
        private DataContext _context;

        public DbConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            _random = new Random(17);
            _context = context;
        }
    }
}
