using DL;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public static class WordsLocationsConvertor
    {
        public static WordsLocations1 ConvertToDto(WordsLocations w)
        {
            return new WordsLocations1()
            {
                Id = w.Id,
                SearchOrSubjectId = w.SearchOrSubjectId,
                //Racheli change
                //  WordId = w.WordId,
                WordLocation = w.WordLocation,
                ItemId = w.ItemId,
                Counter = w.Counter
            };
        }

        public static WordsLocations ConvertToDL(WordsLocations1 w)
        {
            return new WordsLocations()
            {
                Id = w.Id,
                SearchOrSubjectId = w.SearchOrSubjectId,
                //Racheli change
                //  WordId = w.WordId,
                WordLocation = w.WordLocation,
                ItemId = w.ItemId,
                Counter = w.Counter
            };
        }

        public static List<WordsLocations1> ConvertToListDto(List<WordsLocations> lst)
        {
            return lst.Select(w => ConvertToDto(w)).ToList();
        }
    }
}
