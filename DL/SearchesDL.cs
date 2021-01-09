using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DL
{
    public static class SearchesDL
    {
        public static Dictionary<int, List<BookPages>> SearchInBooks(string text)
        {
            Dictionary<int, List<BookPages>> searchResultPages = new Dictionary<int, List<BookPages>>();
            if (text.Length > 0)
            {
                string[] searchWords = text.Split(' ');
                string newRgxText = ".*";
                foreach (var word in searchWords)
                    newRgxText += word + "(.*)";
                var rgx = new Regex(newRgxText);

                List<Items> items = ItemsDL.GetAllItems().Where(item => true == item.EnableSearch).ToList();
                List<BookPages> bookPages = new List<BookPages>();
                foreach (var item in items)
                {
                    bookPages = BookPagesDL.GetBookByItemId(item.ItemId);
                    bookPages = BookPagesDL.SearchText(bookPages, rgx);
                    if (bookPages.Count > 0)
                        searchResultPages.Add(item.ItemId, bookPages);
                }
            }
            return searchResultPages;
        }
        //public static WordsLocations SearchInPreSearch(string text)
        //{
        //    List<PreSearches> preSearches = PreSearchesDL.GetAllPreSerches().Where(pre => pre.PreSearch == text).ToList();
        //    preSearches.ForEach(pre =>
        //    {
        //        pre.SearchedCounter++;
        //        PreSearchesDL.UpdatePreSerches(pre);
        //        //pre.WordsLocations.First().
        //    });

        //}
    }



}

