using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlankApi.DTOs
{
    public class PlankDTO
    {
        [Required]
        public string Titel { get; set; }

        public string Materiaal { get; set; }

        public double Prijs { get; set; }

        public int AantalInStock { get; set; }

        public IList<TagDTO> Tags { get;  set; }
        public double hoogte { get; set; }
        public double breedte { get; set; }
        public double dikte { get; set; }
        public string beschrijving { get; set; }
    }
}
