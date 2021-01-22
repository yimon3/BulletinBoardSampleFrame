﻿using BulletinBoardSampleFrame.Models;
using BulletinBoardSampleFrame.Services;
using BulletinBoardSampleFrame.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoardSampleFrame.Controllers
{
    public class UserController : Controller
    {
        UserService userService = new UserService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public new ActionResult User()
        {
            return View();
        }

        /// <summary>
        /// This is to get user list
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            var userList = userService.getUserList();
            return View("UserList", userList);
        }

        /// <summary>
        /// This is to confirm for new user added to database
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmUser(UserViewModel userViewModel)
        {
            return View("ConfirmUser", userViewModel);
        }

        /// <summary>
        /// This is to save new user
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(UserViewModel userData)
        {
            string ImageName = System.IO.Path.GetFileName(userData.profile);
            string physicalPath = Server.MapPath("~/Images/" + ImageName);
            user newUser = new user();
            newUser.name = userData.name;
            newUser.email = userData.email;
            newUser.password = userData.password;
            if (userData.type == "user")
            {
                newUser.type = "0";
            }
            else
            {
                newUser.type = "1";
            }
            newUser.phone = userData.phone;
            newUser.dob = userData.dob;
            newUser.address = userData.address;
            newUser.profile = ImageName;
            userService.SaveUser(newUser);

            return RedirectToAction("UserList",userData);
        }

        /// <summary>
        /// This is to return user profile
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        public ActionResult UserProfile(UserViewModel userView)
        {
            userView.name = "Mg Mg";
            userView.email = "mgmg@gmail.com";
            userView.type = "user";
            userView.phone = null;
            userView.dob = DateTime.Now;
            userView.phone = "09922333111";
            userView.address = "Yangon";
            userView.profile = "BulletinBoardSampleFrame/Content/Image/Fall Bulletin Boards(16).JPG";
            return View(userView);
        }

        /// <summary>
        /// This is to return edit user view
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// This is for delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("UserList");
        }
    }
}