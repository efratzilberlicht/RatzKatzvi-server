using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace DL
{
    public static class PreSearchesDL
    {
        //Add
        public static PreSearches AddPreSerch(PreSearches preSearch)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                PreSearches isExist = db.PreSearches.FirstOrDefault(x => x.Id == preSearch.Id);
                if (isExist == null)
                {
                  isExist=  db.PreSearches.Add(preSearch);
                    db.SaveChanges();
                }
                return isExist;
            }
        }
        //Delete
        public static void DeletePreSerch(PreSearches preSearch)
        {
            if (preSearch != null)
                using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
                {
                    try {
                        db.Entry(preSearch).State = EntityState.Modified;
                        db.PreSearches.Remove(preSearch);
                        db.SaveChanges();
                    }
                    catch(Exception ex) { }
                    }
        }
        //GetById
        public static PreSearches GeSubjectById(int preSearchId)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.PreSearches.Find(preSearchId);
            }
        }
        //GetAll
        public static List<PreSearches> GetAllPreSerches()
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.PreSearches.ToList();
            }
        }

        //Update
        public static void UpdatePreSerches(PreSearches preSearches)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                try
                {
                    PreSearches isExist = db.PreSearches.FirstOrDefault(x => x.Id == preSearches.Id);
                    if (isExist != null)
                    {
                        db.Entry(preSearches).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex) { }
            }
        }
    }
}



