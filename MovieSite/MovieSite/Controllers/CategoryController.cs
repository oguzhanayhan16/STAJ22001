using BusinessLayer.Concrate;
using DataAccessLayer.EntitiyFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using MovieSite.Models;

namespace MovieSite.Controllers
{
    public class CategoryController : Controller
    {
        GenreManager genre = new GenreManager(new EFGenreRepository()); 

        public IActionResult GenrePage(int id,int userID)
        {

            var model = new MovieListModel()
            {
                userID = userID,
            };

            return View(model);

        }
    
    }
}
