using System.Web.Mvc;

namespace ActionFilterExample.Controllers
{
    public class BookController : Controller
    {

      [Transaction]
      public ActionResult Index()
      {
          return View(DataAccessLayer.GetBooks());
      }

    }
}
