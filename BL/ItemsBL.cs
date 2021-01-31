using BL.Convertors;
using DL;
using Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        //GetItemsForAllSubjects
        public static List<ItemsForSubject> GetItemsForAllSubjects()
        {
            List<Subjects> subjects = SubjectsDL.GetAllSubjects();
            List<ItemsForSubject> itemsForSubjects = new List<ItemsForSubject>();
            foreach (var subject in subjects)
            {
                var result = GetItemsBySubjectId(subject.SubjectId);
                if (result != null)
                    itemsForSubjects.Add(result);
            }
            return itemsForSubjects;
        }
        //GetItemsBySubjectId
        public static ItemsForSubject GetItemsBySubjectId(int subjectId)
        {
            try
            {
                List<Items> lst = new List<Items>(ItemsDL.GetAllItems().Where(i => i.ItemsSubject.FirstOrDefault(s => s.SubjectId == subjectId) != null));
                ItemsForSubject itemsForSubject = new ItemsForSubject();
                var subjectName = SubjectsDL.GeSubjectById(subjectId).Subject;
                var folderpath = $"{HttpContext.Current.Server.MapPath("~/Files/")}{EnumItemsKinds.Bookmarks}";
                if (Directory.Exists(folderpath) && File.Exists($"{folderpath}/{subjectName}"))
                    itemsForSubject.bookmarksPath = $"{folderpath}/{subjectName}";

                folderpath = $"{HttpContext.Current.Server.MapPath("~/Files/")}{EnumItemsKinds.Book}";
                if (Directory.Exists(folderpath) && File.Exists($"{ folderpath}/{subjectName}"))
                    itemsForSubject.bookPath = $"{folderpath}/{subjectName}";

                folderpath = $"{HttpContext.Current.Server.MapPath("~/Files/")}Image";
                if (Directory.Exists(folderpath) && File.Exists($"{ folderpath}/{subjectName}"))
                    itemsForSubject.bookPath = $"{folderpath}/{subjectName}";

                folderpath = $"{HttpContext.Current.Server.MapPath("~/Files/")}{EnumItemsKinds.Lesson}";
                if (Directory.Exists(folderpath) && File.Exists($"{ folderpath}/{subjectName}"))
                    itemsForSubject.bookPath = $"{folderpath}/{subjectName}";

                folderpath = $"{HttpContext.Current.Server.MapPath("~/Files/")}{EnumItemsKinds.Video}";
                if (Directory.Exists(folderpath) && File.Exists($"{ folderpath}/{subjectName}"))
                    itemsForSubject.bookPath = $"{folderpath}/{subjectName}";

                folderpath = $"{HttpContext.Current.Server.MapPath("~/Files/")}{EnumItemsKinds.LessonSummary}";
                if (Directory.Exists(folderpath) && File.Exists($"{ folderpath}/{subjectName}"))
                    itemsForSubject.lessonSummary = FilesDL.GetTextFromFile($"{folderpath}/{subjectName}");
                return itemsForSubject;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Items1> GetItemsBySubjectIdAndKind(int subjectId, int kindId)
        {
            List<Items> lst = new List<Items>(ItemsDL.GetAllItems());
            return ItemsConvertor.ConvertToListDto(lst.Where(i => i.ItemKind == kindId && i.ItemsSubject != null && i.ItemsSubject?.Where(x => x.SubjectId == subjectId).Count() > 0).ToList());
        }

    }
}


