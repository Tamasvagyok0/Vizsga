using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozosKonyvtar.MODEL
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=MSI-JOCO\\DEVELOPER_2022;Database=Vizsgazo1;Trusted_Connection=True;TrustServerCertificate=True;");
            return new ApplicationDbContext(builder.Options);
        }
    }
}
