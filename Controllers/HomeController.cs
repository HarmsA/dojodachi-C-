using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dojodachi.Models;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        private int? FullnessCount
        {
            get {return HttpContext.Session.GetInt32("fullness");}
            set {HttpContext.Session.SetInt32("fullness", (int)value);}
        }
        private int? HappienessCount
        {
            get {return HttpContext.Session.GetInt32("happy");}
            set {HttpContext.Session.SetInt32("happy", (int)value);}
        }
        private int? MealsCount
        {
            get {return HttpContext.Session.GetInt32("meals");}
            set {HttpContext.Session.SetInt32("meals", (int)value);}
        }
        private int? EnergyCount
        {
            get {return HttpContext.Session.GetInt32("energy");}
            set {HttpContext.Session.SetInt32("energy", (int)value);}
        }

        private string WhatPlay
        {
            get {return HttpContext.Session.GetString("whatPlay");}
            set {HttpContext.Session.SetString("whatPlay", value);}
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if(FullnessCount == null)
                FullnessCount = 20;
            ViewBag.Fullness = FullnessCount;

            if(HappienessCount == null)
                HappienessCount = 20;
            ViewBag.Happy = HappienessCount;

            if(MealsCount == null)
                MealsCount = 3;
            ViewBag.Meal = MealsCount;

            if(EnergyCount == null)
                EnergyCount = 50;
            ViewBag.Energy = EnergyCount;

            if(WhatPlay == null)
                WhatPlay = "Welcome to Dojodachi";
            ViewBag.WhatPlay = WhatPlay;
            return View();
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            if(MealsCount <=0)
            {
                WhatPlay = "You need to Work, You are out of food.";
                return RedirectToAction("Index");
            }
            var randnum = 0;
            MealsCount--;
            Random rand = new Random();
            randnum = rand.Next(5,11);
            FullnessCount += randnum;
            WhatPlay = $"You fed you Dojodachi and cost you 1 meal and increased fullness by {randnum}.";
            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            var randnum = 0;
            EnergyCount-=5;
            Random rand = new Random();
            randnum = rand.Next(5,11);
            HappienessCount += randnum;
            WhatPlay = $"You Played with your Dodochi and it cost 5 energy and gained {randnum} happieness.";
            return RedirectToAction("Index");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            if(EnergyCount <=0)
            {
                WhatPlay = "You need to Sleep, you have no energy";
                return RedirectToAction("Index");

            }
            var randnum = 0;
            EnergyCount-=5;
            Random rand = new Random();
            randnum = rand.Next(1,4);
            MealsCount += randnum;
            WhatPlay = $"You worked, it cosed you 5 energy and you earned {randnum} meals.";
            return RedirectToAction("Index");
        }

        [HttpGet("energy")]
        public IActionResult Energy()
        {
            EnergyCount+=15;
            FullnessCount -=5;
            HappienessCount -=5;

            WhatPlay = $"You sleped, this earned you 15 energy but decreased Fullness and Happyniess by 5.";
            return RedirectToAction("Index");
        }

        [HttpGet("restart")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
