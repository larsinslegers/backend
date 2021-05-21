namespace PlankApi.Models
{
    public class CustomerFavorite
    {
        #region Properties
        public int CustomerId { get; set; }

        public int PlankId { get; set; }

        public Customer Customer { get; set; }

        public Plank Plank { get; set; }
        #endregion
    }
}