using Microsoft.EntityFrameworkCore;

namespace ApiForCrud.Model
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Role> roles { get; set; }
        public  DbSet<RolePermission> rolesPermission { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Menu> menu { get; set; }
        public DbSet<TempUser> tempUsers { get; set; }
        public DbSet<PasswordManager> passwordManagers { get; set; }
        public DbSet<OtpManager> otpManagers { get; set; }
        
    }
}
