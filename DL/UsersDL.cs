using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
//using RatzKatzvi.Models;

namespace DL
{
    public static class UsersDL
    {
        //Add
        public static void AddUser(Users user)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        //Update
        public static void UpdateUser(Users user)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        //Delete
        public static void DeleteUser(Users user)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        //GetById
        public static Users GetUserById(int userId)
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.Users.Find(userId);
            }
        }
        //GetAll
        public static List<Users> GetAllUsers()
        {
            using (RatzhKatzviEntities1 db = new RatzhKatzviEntities1())
            {
                return db.Users.ToList();
            }
        }
    }
}
