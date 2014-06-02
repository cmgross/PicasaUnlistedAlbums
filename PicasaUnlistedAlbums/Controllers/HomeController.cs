using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Picasa;
using PicasaUnlistedAlbums.Models;

namespace PicasaUnlistedAlbums.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(PicasaAlbum.GetAlbums());
        }
    }
}