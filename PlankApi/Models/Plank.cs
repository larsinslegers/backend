using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlankApi.Models
{
    public class Plank
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Titel { get; set; }


        public string materiaal { get; set; }

        public double prijs { get; set; }

        public int aantalInStock { get; set; }

        public ICollection<Tag> Tags { get;  set; }
 


        public double hoogte { get; set; }
        public double breedte { get; set; }
        public double dikte { get; set; }
        public string beschrijving { get; set; }
        #endregion

        #region Constructors
        public Plank()
        {
            Tags = new List<Tag>();
           
        }

        public Plank(string name) : this()
        {
            Titel = name;
        }
        #endregion

        #region Methods
        public void AddTag(Tag tag) => Tags.Add(tag);

        public void WeizigAantal(int waarde)
        {
            this.aantalInStock = waarde;
        }

        public Tag GetTag(int id) => Tags.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}