using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class JokesController : Controller
    {
        JokesEntities db = new JokesEntities();
        // GET: Jokes
        public ActionResult Index()
        {
            var jokes = from j in db.Jokes
                        select j;
            return View(jokes.ToList());
        }
    }
}