namespace PlankApi.Models
{
    public class Tag
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        #endregion

        #region Constructors
        public Tag(string name)
        {
            Name = name;
            
        }
        #endregion
    }
}