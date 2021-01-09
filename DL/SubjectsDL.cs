using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RatzKatzvi.Models;

namespace DL
{
    public static class SubjectsDL
    {
        //Add
        public static void AddSubject(Subjects subject)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
            }
        }
        //Update
        public static void UpdateSubject(Subjects subject)
        {
            try
            {


                using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
                {
                    //Subjects s = db.Subjects.FirstOrDefault(s1 => s1.SubjectId == subject.SubjectId);
                    //s.Subject = subject.Subject;
                    //s.SearchedCounter = subject.SearchedCounter;
                    //s.Parent = subject.Parent;
                    db.Entry(subject).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        //Delete
        public static void DeleteSubject(Subjects subject)
        {
            if (subject?.SubjectId != null && subject?.SubjectId != 0)
                using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
                {
                    try
                    {
                        db.Entry(subject).State = EntityState.Deleted;
                        db.Subjects.Remove(subject);
                        db.SaveChanges();
                    }
                    catch (Exception ex) { }
                }
        }
        //GetById
        public static Subjects GeSubjectById(int subjectId)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.Subjects.Find(subjectId);
            }
        }
        //GetOnlyParents
        public static List<Subjects> GeSubjectOnlyParents()
        {
            try
            {
                using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
                {
                    try
                    {
                        return db.Subjects.Where(sbjct => sbjct.Parent == 0).ToList();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        //GetByParentId
        public static List<Subjects> GeSubjectByParentId(int parentId)
        {
            try
            {
                using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
                {
                    try
                    {
                        return db.Subjects.Where(sbjct => sbjct.Parent == parentId).ToList();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        //GetAll
        public static List<Subjects> GetAllSubjects()
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.Subjects.ToList();
            }
        }
    }
}
