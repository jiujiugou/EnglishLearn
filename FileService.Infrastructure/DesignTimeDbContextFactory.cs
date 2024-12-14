using CommonInitializer;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FSDbContext>
    {
        public FSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = DbContextOptionsBuilderFactory.Create<FSDbContext>();
            return new FSDbContext(optionsBuilder.Options, null);
        }
    }
}
