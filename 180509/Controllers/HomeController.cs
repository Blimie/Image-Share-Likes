using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using _180509.Data;
using _180509.Models;

namespace _180509.Controllers
{
    public class HomeController : Controller
    {
        ImagesRepository repo = new ImagesRepository(Properties.Settings.Default.ConStr);
        public ActionResult Index()
        {
            return View(repo.GetImages());
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(Image image, HttpPostedFileBase imageFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            imageFile.SaveAs(Server.MapPath("~/UploadedImages/") + fileName);
            image.FileName = fileName;
            repo.AddImage(image);
            return Redirect("Index");
        }
        public ActionResult ViewImage(int imageId)
        {
            var viewModel = new ViewImageViewModel { Image = repo.GetById(imageId) };
            if (HasPermissionToLike(imageId))
            {
                viewModel.HasPermissionToLike = true;
            }
            else
            {
                viewModel.HasPermissionToLike = false;
            }
            return View(viewModel);
        }
        private bool HasPermissionToLike(int imageId)
        {
            if (Session["likedids"] == null)
            {
                return true;
            }
            var likedIds = (List<int>)Session["likedids"];
            return !likedIds.Contains(imageId);
        }
        [HttpPost]
        public void IncrementLikeCount(int imageId)
        {
            repo.IncrementLikeCount(imageId);

            List<int> likedIds;
            if (Session["likedids"] == null)
            {
                likedIds = new List<int>();
                Session["likedids"] = likedIds;
            }
            else
            {
                likedIds = (List<int>)Session["likedids"];
            }

            likedIds.Add(imageId);
        }
        public ActionResult GetLikeCount(int imageId)
        {
            return Json(new { Likes = repo.GetById(imageId).LikeCount }, JsonRequestBehavior.AllowGet);
        }
    }
}