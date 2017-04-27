using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace paylive.Console.DbContext
{
    public class LiveContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<WebImConfig> WebImConfig { get; set; }
        public DbSet<SmsQu> SmsQu { get; set; }
        public DbSet<Receivers> Receivers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"server=139.199.20.165;uid=root;pwd=t4kO23u8I6aT;database=live;");
    }
}