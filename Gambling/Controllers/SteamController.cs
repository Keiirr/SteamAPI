using Gambling.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Gambling.Controllers
{
    public class SteamController : Controller
    {
        // GET: Steam
        public ActionResult Index()
        {
            return View();
        }

        public string GetSteamID()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var CurrentUser = manager.FindById(User.Identity.GetUserId());
            if (User.Identity.Name != "")
            {
                string url = CurrentUser.Logins.First().ProviderKey;
                ViewBag.steamid = url.Split('/')[5]; //here we going to split the providerkey so we get the right part
            }
            else
            {
                ViewBag.steamid = "";
            }

            return ViewBag.steamid;
        }

        //SteamID For Testing
        //76561198011967714

        [HttpGet]
        public ContentResult GetUserStatsForGame()
        {
            string url = string.Format("http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v2/?appid=730&key=C0AB6EBDB7B6C63D2F3AE61A7870F374&steamid={0}", this.GetSteamID());
            string result = null;

            using (var client = new WebClient())
            {
                result = client.DownloadString(url);
            }

            return Content(result, "application/json");
        }

        [HttpGet]
        public ContentResult GetProfile()
        {
            string url = string.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=C0AB6EBDB7B6C63D2F3AE61A7870F374&steamids={0}", this.GetSteamID());
            string result = null;

            using (var client = new WebClient())
            {
                result = client.DownloadString(url);
            }
            return Content(result, "application/json");
        }
    }
}