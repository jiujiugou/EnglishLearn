using CommonInitializer;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaEncoder.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MEDbContext>
    {
        public MEDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = DbContextOptionsBuilderFactory.Create<MEDbContext>();
            return new MEDbContext(optionsBuilder.Options, null);
        }
    }
}
