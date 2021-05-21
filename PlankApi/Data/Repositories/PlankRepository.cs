using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
 using PlankApi.Models;


namespace PlankApi.Data.Repositories
{
    public class PlankRepository  : IPlankRepository
    {
          private readonly PlankContext _context;
         private readonly DbSet<Plank> _planken;

         public PlankRepository(PlankContext dbContext)
         {
             _context = dbContext;
            _planken = dbContext.Planken;
         }

         public IEnumerable<Plank> GetAll()
         {
             return _planken.Include(r=> r.Tags).ToList();
         }

         public Plank GetBy(int id)
         {
             return _planken.Include(r => r.Tags).SingleOrDefault(r => r.Id == id);
         }

         public bool TryGetPlank(int id, out Plank plank)
         {
            plank = _context.Planken.Include(t => t.Tags).FirstOrDefault(t => t.Id == id);
             return plank != null;
         }

         public void Add(Plank plank)
         {
             _planken.Add(plank);
         }

         public void Update(Plank plank)
         {
             _context.Update(plank);
         }

         public void Delete(Plank plank)
         {
             _planken.Remove(plank);
         }

         public void SaveChanges()
         {
             _context.SaveChanges();
         }
        public IEnumerable<Plank> GetBy(string titel = null, string materiaal = null, string tag = null)
        {
            var planken = _planken.Include(r => r.Tags).AsQueryable();
            if (!string.IsNullOrEmpty(titel))
                planken = planken.Where(r => r.Titel.IndexOf(titel) >= 0);
            if (!string.IsNullOrEmpty(materiaal))
                planken = planken.Where(r => r.materiaal == materiaal);
            if (!string.IsNullOrEmpty(tag))
                planken = planken.Where(r => r.Tags.Any(i => i.Name == tag));
            return planken.OrderBy(r => r.Titel).ToList();
        }
    }
 }