using Microsoft.AspNetCore.Mvc;

namespace FanusYazılım.Controllers
{
    public class MainPageController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult Calismalar()
        {
           
            return PartialView();
        }

        public IActionResult Index2()
        {
            return View();
        }
        public PartialViewResult GetAllItems()
        {
           
            return PartialView("_GetAllItems");
        }

        public PartialViewResult IcerikveKullanicilar()
        {
         
            return PartialView();
        }


        public PartialViewResult Yorumlar()
        {

            return PartialView();
        }


        public IActionResult Robotik()
        {
            return View();
        }

        public IActionResult Website()
        {
            return View();
        }
        public IActionResult OyunProgramlama()
        {
            return View();
        }
        public IActionResult Hikayeci()
        {
            return View();
        }

        public IActionResult ZamanMakinası()
        {
            return View();
        }

        public IActionResult Tur()
        {
            return View();
        }

        public IActionResult Muze()
        {
            return View();
        }

        public IActionResult SosyalMedya()
        {
            return View();
        }
    }
}
