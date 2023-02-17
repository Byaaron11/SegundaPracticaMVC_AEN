using Microsoft.AspNetCore.Mvc;
using SegundaPracticaMVC_AEN.Models;
using SegundaPracticaMVC_AEN.Repositories;

namespace SegundaPracticaMVC_AEN.Controllers
{
    public class ComicsController : Controller
    {
        private IRepository repo;

        public ComicsController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Comic comic)
        {
            this.repo.InsertarComic(comic.Nombre, comic.Imagen, comic.Descripcion);
            return RedirectToAction("Index");
        }
    }
}
