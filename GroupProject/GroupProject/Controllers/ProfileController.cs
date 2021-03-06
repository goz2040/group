﻿using GroupProject.Models;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        ArtWorksRepository _artWorksRepository = new ArtWorksRepository();

        
        // GET: Profile
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ProfileViewModel vm = new ProfileViewModel();

            ApplicationUser LogedInUser = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                LogedInUser = db.Users.SingleOrDefault(i => i.Id == userId);
            }

            vm.ApplicationUser = LogedInUser;
            vm.Artworks = _artWorksRepository.GetArtWorksByArtist(userId).ToList(); ;
            return View(vm);
            //return View();
        }

        public ActionResult Edit()
        {

            UserRoleViewModel u = new UserRoleViewModel();

            string userId = User.Identity.GetUserId();
            
            ApplicationUser applicationUser = db.Users.Find(userId);
            u.ApplicationUser = applicationUser;

            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRoleViewModel user)
        {
            string path = "";

            if (user.RegisterViewModel.ProfilePicture != null)
            {
                user.ApplicationUser.ProfilePicture = Path.GetFileName(user.RegisterViewModel.ProfilePicture.FileName);

                path = Path.Combine(Server.MapPath("~/ProfilePictures"), user.ApplicationUser.ProfilePicture);
                user.RegisterViewModel.ProfilePicture.SaveAs(path);
            }

            if (ModelState.IsValid)
            {
                db.Users.Attach(user.ApplicationUser);
                db.Entry(user.ApplicationUser).State = EntityState.Modified;
                db.SaveChanges();               
            }
            return RedirectToAction("Index");
            //return View(applicationUser);
        }

        //public ActionResult Edit(ApplicationUser user)
        //{           
        //    string path = "";

        //    if (user.ProfilePicture != null)
        //    {
        //        path = Path.Combine(Server.MapPath("~/ProfilePictures"),
        //        user.ProfilePicture.FileName);
        //        user.ProfilePicture.SaveAs(path);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Attach(user);
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();

        //    }
        //    return RedirectToAction("Index");
        //    //return View(applicationUser);
        //}

        public ActionResult Artworks()
        {
            string userId = User.Identity.GetUserId();
            ProfileViewModel vm = new ProfileViewModel();

            ApplicationUser LogedInUser = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                LogedInUser = db.Users.SingleOrDefault(i => i.Id == userId);
            }

            vm.ApplicationUser = LogedInUser;
            vm.Artworks = _artWorksRepository.GetArtWorksByArtist(userId).ToList(); ;
            return View(vm);
        }


    }
}