using Microsoft.EntityFrameworkCore;
using PlankApi.Models;
using System.Linq;

namespace PlankApi.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PlankContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(PlankContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Customers;
        }

        public Customer GetBy(string email)
        {
            return _customers.Include(c => c.Favorites).ThenInclude(f => f.Plank).ThenInclude(r => r.Tags).SingleOrDefault(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
