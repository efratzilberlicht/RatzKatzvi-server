using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DL
{
    public static class BookPagesDL
    {
        //Add
        public static void AddBook(List<BookPages> book)
        {
            List<BookPages> isExistBook = GetBookByItemId(book.First().ItemId);
            if (isExistBook == null)
                using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
                {
                    foreach (BookPages page in book)
                    {
                        try
                        {
                            db.BookPages.Add(page);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    db.SaveChanges();
                }
        }
        public static void AddBookByItem(Items item)
        {
            string url = string.Empty;
            switch (item.ItemKind)
            {
                case 1:
                    url = HttpContext.Current.Server.MapPath("~\\Files\\Books\\" + item.ItemName);
                    break;
                case 4:
                    url = HttpContext.Current.Server.MapPath("~\\Files\\Articals\\" + item.ItemName);
                    break;
                default: return;
            }
            List<BookPages> book = ConvertDocToObject(url, item.ItemId);
            AddBook(book);
        }
        //Update
        public static void UpdateBook(List<BookPages> book)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                try
                {
                    List<BookPages> isExistBook = GetBookByItemId(book.First().ItemId);
                    if (isExistBook != null)
                    {
                        foreach (var page in isExistBook)
                        {
                            try
                            {
                                db.BookPages.AddOrUpdate(page);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                throw;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex) { }
            }
        }
        //DeleteByItemId
        public static void DeleteBookByItemId(int ItemId)
        {
            List<BookPages> book = GetBookByItemId(ItemId);
            DeleteBook(book);
        }
        //DeleteBook
        public static void DeleteBook(List<BookPages> book)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                foreach (var page in book)
                {
                    try
                    {
                        db.Entry(page).State = EntityState.Modified;
                        db.BookPages.Remove(page);

                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
                db.SaveChanges();
            }
        }
        //GetById  
        //public static List<BookPages> GetBookBySubjectIdOrPresearchId(List<int> subjectIds, List<int> presearchIds)
        //{
        //    using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
        //    {
        //        return db.BookPages.Where(i => subjectIds.Contains(i. ?? 0) || presearchIds.Contains(i.SearchId ?? 0)).OrderBy(book=>book.SubjectId).ThenBy(book=>book.SearchId).ToList();
        //    }
        //}
        //GetById    
        public static List<BookPages> GetBookByItemId(int itemId)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.BookPages.Where(i => i.ItemId == itemId).ToList();
            }
        }
        //GetAll
        public static List<BookPages> GetAllBooks()
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.BookPages.ToList();
            }
        }
        //GetPage
        public static BookPages GetPage(int itemId, int page)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                try
                {
                    return db.BookPages.Find(itemId, page);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
        }
        //ConvertPdftoStringArray  
        private static List<BookPages> ConvertDocToObject(string url, int itemId)
        {
            List<BookPages> book = new List<BookPages>();
            using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(url))
            {
                string page;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    page = Reverse(PdfTextExtractor.GetTextFromPage(reader, i));
                    book.Add(new BookPages { ItemId = itemId, BookPage = i + 1, Text = page });
                }
            }
            return book;
        }
        private static string Reverse(string s)
        {

            var correctPage = "";
            if (s != "")
            {
                string[] st = s.Split('\n');
                for (int i = 0; i < st.Length; i++)
                {
                    char[] charArray = st[i].ToCharArray();
                    Array.Reverse(charArray);
                    string correctLine = new string(charArray);
                    if (correctLine != "" && correctLine.IsNormalized())
                        correctPage += "\n" + correctLine;
                    else if (correctLine != "")
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        Encoding latinEncoding = Encoding.GetEncoding("Windows-1252");
                        Encoding hebrewEncoding = Encoding.GetEncoding("Windows-1255");
                        byte[] latinBytes = latinEncoding.GetBytes(correctLine);
                        string hebrewString = hebrewEncoding.GetString(latinBytes);
                        correctPage += "\n" + hebrewString;
                    }
                }
            }
            return correctPage;
        }

        public static List<BookPages> SearchText(List<BookPages> book, Regex regex)
        {
            List<BookPages> updateBook = new List<BookPages>();
            foreach (BookPages page in book)
            {
                if (regex.IsMatch(page.Text))
                    updateBook.Add(page);
            }

            return updateBook;

        }
    }
}