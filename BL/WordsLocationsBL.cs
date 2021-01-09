using DL;
using Dto;
using BL.Convertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class WordsLocationsBL
    {
        //Add 
        public static void AddWordsLocation(WordsLocations1 wordsLocation)
        {
            WordsLocationsDL.AddWordsLocation(WordsLocationsConvertor.ConvertToDL(wordsLocation));
        }
        //Update
        public static void UpdateWordsLocation(WordsLocations1 wordsLocation)
        {
            WordsLocationsDL.UpdateWordsLocation(WordsLocationsConvertor.ConvertToDL(wordsLocation));
        }
        //Delete האם יש למחוק קודם מהטבלאות שהוא נמצא בהן בקשרי גומלין
        public static void DeleteWordsLocation(WordsLocations1 wordsLocation)
        {
            WordsLocationsDL.DeleteWordsLocation(WordsLocationsConvertor.ConvertToDL(wordsLocation));
        }
        //DeleteByItemId
        public static void DeleteWordsLocationsByItemId(int itemId)
        {
            List<WordsLocations> lst = new List<WordsLocations>(WordsLocationsDL.GetAllWordsLocations());
            if (lst.Count > 0)
                WordsLocationsDL.DeleteWordsLocation(lst.Where(wrd => wrd.ItemId.Equals(itemId)).FirstOrDefault());
        }
        //GetById
        public static WordsLocations1 GetWordsLocationById(int id)
        {
            return WordsLocationsConvertor.ConvertToDto(WordsLocationsDL.GeWordsLocationById(id));
        }
        //GetByWordId
        //public static List<WordsLocations1> GetWordsLocationByWordId(int wordId)
        //{
        //    List<WordsLocations> lst = new List<WordsLocations>(WordsLocationsDL.GetAllWordsLocations());
        //    return WordsLocationsConvertor.ConvertToListDto(lst.Where(k => k.WordId.Equals(wordId)).ToList());
        //}
        //GetAllWordsLocations
        public static List<WordsLocations1> GetAllWordsLocations()
        {
            return WordsLocationsConvertor.ConvertToListDto(WordsLocationsDL.GetAllWordsLocations());
        }
    }
}
