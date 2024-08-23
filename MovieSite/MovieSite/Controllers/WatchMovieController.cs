using EntityLayer.Concrete;
using DataAccessLayer.Concrate;
using BusinessLayer.ValidationRules;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using DataAccessLayer.EntitiyFramework;
using BusinessLayer.Concrate;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using MovieSite.Models;
namespace MovieSite.Controllers
{
    public class WatchMovieController : Controller
    {
        MovieManager mm = new MovieManager(new EFMovieRepository());
        UserManager um = new UserManager(new EFUserRepository());
        CommentManager cm = new CommentManager(new EFCommentRepository());
        CollectionManager cl = new CollectionManager(new EFCollectionRepository());
        Context c = new Context();
        public IActionResult Index(int UserID, int MovieID)
        {
            var movie = c.Movies.Where(x => x.MovieID == MovieID).Select(y => y.Name).FirstOrDefault();
            ViewBag.v = movie;
            ViewBag.v1 = MovieID;
            ViewBag.v2 = UserID;

            var colletion = cl.GetList();

            var viewModel = new MovieViewModel
            {
                Users = um.getUser(UserID),
                collec =colletion,
                com = new Comment()

            };
            return View(viewModel);

            

        }
        [HttpPost]
        public IActionResult Index(MovieViewModel viewModel, int MovieID, int UserID)
        {
            CommentValidator validator = new CommentValidator();
            ValidationResult results = validator.Validate(viewModel.com);

            if (results.IsValid)
            {
                viewModel.com.CommentTittle = "gereksiz";
                viewModel.com.CommentDate = DateTime.Now;
                viewModel.com.CommentStatus = true;
                viewModel.com.Likes = 0;
                viewModel.com.MovieID = MovieID;
                viewModel.com.UserID = UserID;

                cm.TAdd(viewModel.com);
                return RedirectToAction("Index", new { UserID = UserID, MovieID = MovieID });
            }

            return View(viewModel); // Hata durumunda formu tekrar göster
        }


    }
}
