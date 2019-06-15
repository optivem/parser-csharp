﻿using Microsoft.EntityFrameworkCore;
using Optivem.Template.Infrastructure.EntityFrameworkCore.Customers;

namespace Optivem.Template.Infrastructure.EntityFrameworkCore
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerRecord> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: VC: Check if needed
            // modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.ApplyConfiguration(new CustomerRecordConfiguration());

            // TODO: VC: Dynamically find everything implementing IEntityTypeConfiguration interface
        }
    }
}