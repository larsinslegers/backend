using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlankApi.Models
{
    public class Customer
    {
        #region Properties
        //add extra properties if needed
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<CustomerFavorite> Favorites { get; private set; }

        public IEnumerable<Plank> FavoritePlanken => Favorites.Select(f => f.Plank);
        #endregion

        #region Constructors
        public Customer()
        {
            Favorites = new List<CustomerFavorite>();
        }
        #endregion

        #region Methods
        public void AddFavoriteRecipe(Plank plank)
        {
            Favorites.Add(new CustomerFavorite() { PlankId = plank.Id, CustomerId = CustomerId, Plank = plank, Customer = this });
        }
        #endregion
    }
}
