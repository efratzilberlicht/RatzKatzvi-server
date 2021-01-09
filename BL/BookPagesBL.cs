using BL.Convertors;
using DL;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BookPagesBL
    {
        //Add
        public static void AddBook(List<BookPages1> book)
        {
            List<BookPages> newBook = BookPagesConvertor.ConvertToListDL(book);
            BookPagesDL.AddBook(newBook);
        }


        public static void AddBookByItem(Items item)
        {
            BookPagesDL.AddBookByItem(item);
        }
        //DeleteBook
        public static void DeleteBook(List<BookPages1> book)
        {

            List<BookPages> newBook = BookPagesConvertor.ConvertToListDL(book);
            BookPagesDL.DeleteBook(newBook);
        }
        public static void DeleteBookByItemId(int ItemId)
        {
            BookPagesDL.DeleteBookByItemId(ItemId);
        }
        //Update
        public static void UpdateBooks(List<BookPages1> book)
        {
            List<BookPages> newBook = BookPagesConvertor.ConvertToListDL(book);
            BookPagesDL.UpdateBook(newBook);
        }
        //GetById
        public static List<BookPages1> GetBookById(int itemId)
        {
            return BookPagesConvertor.ConvertToListDto(BookPagesDL.GetBookByItemId(itemId));
        }
        //GetAll
        public static List<BookPages1> GetAllBooks()
        {
            return BookPagesConvertor.ConvertToListDto(BookPagesDL.GetAllBooks());
        }
        //GetPage
        public static BookPages1 GetPage(int itemId, int page)
        {
            return BookPagesConvertor.ConvertToDto(BookPagesDL.GetPage(itemId, page));
        }
    }
}
