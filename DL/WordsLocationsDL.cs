using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RatzKatzvi1.Models;

namespace DL
{
    public static class WordsLocationsDL
    {
        //Add
        public static void AddWordsLocation(WordsLocations wordsLocation)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                db.WordsLocations.Add(wordsLocation);
                db.SaveChanges();
            }
        }
        //Update
        public static void UpdateWordsLocation(WordsLocations wordsLocation)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                db.Entry(wordsLocation).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        //Delete
        public static void DeleteWordsLocation(WordsLocations wordsLocation)
        {
            if (wordsLocation != null)
                using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
                {
                    try
                    {
                        db.WordsLocations.Remove(wordsLocation);
                        db.SaveChanges();
                    }
                    catch (Exception ex) { }
                }
        }
        //GetById
        public static WordsLocations GeWordsLocationById(int Id)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.WordsLocations.Where(w => w.Id == Id).FirstOrDefault();
            }
        }
        //GetAll
        public static List<WordsLocations> GetAllWordsLocations()
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.WordsLocations.ToList();
            }
        }
    }
}
