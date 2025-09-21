using Microsoft.EntityFrameworkCore;


namespace EmployeeAPI.Data
{
    public class EmployeeAppContext : DbContext
    {
        public EmployeeAppContext(DbContextOptions<EmployeeAppContext>options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
