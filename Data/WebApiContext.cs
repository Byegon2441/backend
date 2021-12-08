using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Data
{
    public class WebApiContext : DbContext
    {

        public DbSet<PaymentDetail> PaymentDetails { get; set;}
        public DbSet<Bank> banks { get; set;}
        

        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}