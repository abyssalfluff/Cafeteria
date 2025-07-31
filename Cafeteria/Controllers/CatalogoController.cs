using System.Linq;
using System.Web.Mvc;
using Cafeteria.Models;

namespace Cafeteria.Controllers
{
    public class CatalogoController : Controller
    {
        private cafeteriaEntities db = new cafeteriaEntities();

        public ActionResult Index()
        {
            var productos = db.Productos.ToList();
            return View(productos);
        }
    }
}
