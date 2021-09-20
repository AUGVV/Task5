using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task5.Models;

namespace Task5.Pages
{
    public class GamePageModel : PageModel
    {

        private GameContext GameDb;

        public GamePageModel(GameContext GameContext)
        {
            GameDb = GameContext;
        }

        public List<string> List = new List<string>() {"Камень","Ножницы","Бумага","Спок","Кок","Рок","Пок"};

        public IActionResult OnGet()
        {
            int id=0;
            if (GameDb.Games.Where(x => x.Unique == HttpContext.Session.Id)?.FirstOrDefault()?.Unique == HttpContext.Session.Id)
            {
                id = (int)(GameDb.Games.Where(x => x.Unique == HttpContext.Session.Id)?.FirstOrDefault()?.id);
            }
            else if (GameDb.Games.Where(x => x.GameCompanionId == HttpContext.Session.Id)?.FirstOrDefault()?.GameCompanionId == HttpContext.Session.Id)
            {
                id = (int)GameDb.Games.Where(x => x.GameCompanionId == HttpContext.Session.Id)?.FirstOrDefault()?.id;
            }
            GameModel Game = GameDb.Games.Where(x => x.id == id).FirstOrDefault();
            if (Game == null)
            {
                return Redirect("/ChoisePage");
            }
            GameInitialization();     
            ViewData["Youname"] = HttpContext.Session.GetString("UserName").ToString();
            return Page();
        }

        void GameInitialization()
        {
            if (GameDb.Games.Where(x => x.Unique == HttpContext.Session.Id)?.FirstOrDefault()?.Unique == HttpContext.Session.Id)
            {
                ViewData["NameCreator"] = GameDb.Games.Where(x => x.Unique == HttpContext.Session.Id)?.FirstOrDefault()?.GameCreator.ToString();
                ViewData["GameId"] = GameDb.Games.Where(x => x.Unique == HttpContext.Session.Id)?.FirstOrDefault()?.id;
                SetRules(GameDb.Games.Where(x => x.Unique == HttpContext.Session.Id)?.FirstOrDefault()?.Rules);
            }
            else if(GameDb.Games.Where(x => x.GameCompanionId == HttpContext.Session.Id)?.FirstOrDefault()?.GameCompanionId == HttpContext.Session.Id)
            {
                ViewData["NameCreator"] = GameDb.Games.Where(x => x.GameCompanionId == HttpContext.Session.Id)?.FirstOrDefault()?.GameCreator.ToString();
                ViewData["GameId"] = GameDb.Games.Where(x => x.GameCompanionId == HttpContext.Session.Id)?.FirstOrDefault()?.id;
                SetRules(GameDb.Games.Where(x => x.GameCompanionId == HttpContext.Session.Id)?.FirstOrDefault()?.Rules);
            }
            else
            {
                ViewData["NameCreator"] = "NaN";
            }
        }

        void SetRules(string rules)
        {
            if (rules == "3 хода")
            {
                ViewData["CountOfSteps"] = 3;
            }
            else if (rules == "5 ходов")
            {
                ViewData["CountOfSteps"] = 5;
            }
            else if (rules == "7 ходов")
            {
                ViewData["CountOfSteps"] = 7;
            }
        }

        public IActionResult OnPost(int id)
        {
            GameModel Game = GameDb.Games.Where(x=>x.id == id).FirstOrDefault();
            if (Game != null)
            {
                GameDb.Games.Remove(Game);
                GameDb.SaveChanges();
            }
            return Redirect("/ChoisePage");

        }
    }
}
