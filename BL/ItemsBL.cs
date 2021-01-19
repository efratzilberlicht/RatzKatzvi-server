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
    public static class ItemsBL
    {
        //Add
        public static void AddItem(Items1 item)
        {
            Items newItem = ItemsConvertor.ConvertToDL(item);
            try
            {
                ItemsDL.AddItem(newItem);
                if (newItem.EnableSearch)
                {
                    BL.BookPagesBL.AddBookByItem(newItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        //Delete 
        public static void DeleteItem(Items1 item)
        {

            Items newItem = ItemsConvertor.ConvertToDL(item);
            ItemsSubjectBL.DeleteItemsSubjectByItemId(newItem.ItemId);
            //TODO
            //WordsLocationsBL.DeleteWordsLocationsByItemId(newItem.ItemId);
            BookPagesBL.DeleteBookByItemId(newItem.ItemId);

            ItemsDL.DeleteItem(newItem);
        }
        //Update
        public static void UpdateItem(Items1 item)
        {
            Items newItem = ItemsConvertor.ConvertToDL(item);
            ItemsDL.UpdateItem(newItem);
        }
        //GetById
        public static Items1 GetItemById(int item)
        {
            return ItemsConvertor.ConvertToDto(ItemsDL.GetItemById(item));
        }
        //GetByName
        public static Items1 GetItemByName(string item)
        {

            List<Items> lst = new List<Items>(ItemsDL.GetAllItems());

            return ItemsConvertor.ConvertToDto(lst.Where(i => i.ItemName.Equals(item)).FirstOrDefault());
        }
        //GetAll
        public static List<Items1> GetAllItems()
        {

            return ItemsConvertor.ConvertToListDto(ItemsDL.GetAllItems());
        }
        //GetAllByKind
        public static List<Items1> GetAllVideos(int kindId, int pageNum)
        {
            const int numVideoInPage = 10;
            List<Items> lst = new List<Items>(ItemsDL.GetAllItems());
            return ItemsConvertor.ConvertToListDto(lst.Where(i => i.ItemKind == kindId).Skip(numVideoInPage * pageNum).Take(numVideoInPage).ToList());
        }
        //TODO
        public static List<Items1> GetAllByKind(int kindId)
        {
            List<Items> lst = new List<Items>(ItemsDL.GetAllItems());
            return ItemsConvertor.ConvertToListDto(lst.Where(i => i.ItemKind == kindId).ToList());
        }

        // GetAllBySubjectId
        public static List<Items1> GetAllBySubjectId(int subjectId)
        {
            List<Items> lst = new List<Items>(ItemsDL.GetAllItems());
            return ItemsConvertor.ConvertToListDto(lst.Where(i => i.ItemsSubject != null && i.ItemsSubject?.Where(x => x.SubjectId == subjectId).Count() > 0).ToList());
        }

    }
}


