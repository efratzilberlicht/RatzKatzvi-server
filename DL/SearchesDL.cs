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
        public static List<WordLocation> SearchInBooks(string text)
        {
            if (text.Length > 0)
            {
                string[] searchWords = text.Split(' ');
                string newRgxText = ".*";
                foreach (var word in searchWords)
                    newRgxText += word + "(.*)";
                var rgx = new Regex(newRgxText);

                List<Items> items = ItemsDL.GetAllItems().Where(item => true == item.EnableSearch).ToList();
                List<BookPages> bookPages = new List<BookPages>();
                List<WordLocation> searchResultWordLocations = new List<WordLocation>();
                bool flag = false;
                PreSearches newPreSearch = new PreSearches();
                foreach (var item in items)
                {
                    bookPages = BookPagesDL.GetBookByItemId(item.ItemId);
                    bookPages = BookPagesDL.SearchText(bookPages, rgx);
                    if (bookPages.Count > 0)
                    {
                        if (!flag)
                        {
                            flag = true;
                            newPreSearch = PreSearchesDL.AddPreSerch(new PreSearches { PreSearch = text, SearchedCounter = 1 });
                        }
                        if (newPreSearch != default(PreSearches))
                        {
                            searchResultWordLocations.AddRange(WordLocationDL.Create_List_WordLocationFromBooPage(bookPages, newPreSearch.Id));
                        }
                    }

                }
                if (searchResultWordLocations != default(List<WordLocation>))
                    WordLocationDL.AddList(searchResultWordLocations);
                return searchResultWordLocations;
            }
            return new List<WordLocation>();

        }
    }



}

