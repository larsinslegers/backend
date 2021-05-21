using Microsoft.AspNetCore.Identity;
using PlankApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PlankApi.Data
{
    public class PlankDataInitializer
    {
        private readonly PlankContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PlankDataInitializer(PlankContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with recipes, see DBContext         
                Customer customer = new Customer { Email = "admin@admin.be", FirstName = "Ad", LastName = "Min" };
                _dbContext.Customers.Add(customer);
                await CreateUser(customer.Email, "P@ssword1111");
                Customer student = new Customer { Email = "student@hogent.be", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Customers.Add(student);
                student.AddFavoriteRecipe(_dbContext.Planken.First());
                await CreateUser(student.Email, "P@ssword1111");
                _dbContext.SaveChanges();
            }
        }
        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
