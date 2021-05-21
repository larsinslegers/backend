using System.Collections.Generic;

namespace PlankApi.Models
{
    public interface IPlankRepository
    {
        Plank GetBy(int id);
        IEnumerable<Plank> GetBy(string titel = null, string materiaal = null, string tag = null);
        bool TryGetPlank(int id, out Plank plank);
        IEnumerable<Plank> GetAll();
        void Add(Plank plank);
        void Delete(Plank plank);
        void Update(Plank plank);
        void SaveChanges();
    }
}