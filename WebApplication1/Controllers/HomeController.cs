﻿using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)] - for parallel work
    public class HomeController : Controller
    {
        JokesEntities db = new JokesEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult btnRandomJoke_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int randomJokeId = rand.Next(1, db.Jokes.Count() + 1);//from 1 to size of Jokes database
            string message = "Не съществуват вицове в базата данни.";
            foreach(var item in db.Jokes)
            {
                if (item.Id == randomJokeId)
                {
                    string cont = item.Content;
                    cont = cont.Replace("br", "<br>");
                    message = "<br>" + cont;
                    break;
                }
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetJokesByCategory(string category)
        {
            string jokes = "";
            foreach (var item in db.Jokes)
            {
                if (item.Categories.Contains(category) || item.Content.Contains(category))
                {
                    string cont = item.Content;
                    cont = cont.Replace("br", "<br>");
                    jokes += "<br>" + cont + "<br>";
                }
            }
            if(jokes=="")
            {
                jokes = "Не съществуват вицове от тази категория.";
            }
            return new JsonResult { Data = jokes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        public void AddJokeToDatabase(string content, string categories)
        {
            Jokes joke = new Jokes();
            joke.Content = content;
            joke.Categories = categories;
            db.Jokes.Add(joke);
            db.SaveChanges();
        }
    }
}