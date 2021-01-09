﻿using BL.Convertors;
using DL;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class UsersBL
    {
        //Add
        public static void AddUser(Users1 user)
        {
            Users newUser = UsersConvertor.ConvertToDL(user);
            UsersDL.AddUser(newUser);
        }
        //Update
        public static void UpdateUser(Users1 user)
        {
            UsersDL.UpdateUser(UsersConvertor.ConvertToDL(user));
        }
        //Delete האם יש למחוק קודם מהטבלאות שהוא נמצא בהן בקשרי גומלין
        public static void DeleteUser(Users1 user)
        {
            Users newUser = UsersConvertor.ConvertToDL(user);
            UsersDL.DeleteUser(newUser);
        }
        //LogIn1
        public static Users1 Login1(string userName, string password)
        {
            return UsersConvertor.ConvertToDto(
                UsersDL.GetAllUsers().FirstOrDefault(u => u.UserName.Equals(userName) && u.Password.Equals(password)));
        }
        //LogIn2
        public static Users1 Login2(string email,string password)
        {
            return UsersConvertor.ConvertToDto(
                UsersDL.GetAllUsers().FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password)));
        }
        //GetUserById
        public static Users1 GetUserById(int userId)
        {
            return UsersConvertor.ConvertToDto(UsersDL.GetUserById(userId));
        }
        //GeByUserName
        public static Users1 GeByUserName(string userName)
        {
            return UsersConvertor.ConvertToDto(UsersDL.GetAllUsers().FirstOrDefault(u => u.UserName.Equals(userName)));
        }
        //GetAllUsers
        public static List<Users1> GetAllUsers()
        {
            return UsersConvertor.ConvertToListDto(UsersDL.GetAllUsers());
        }
        
    }
}



