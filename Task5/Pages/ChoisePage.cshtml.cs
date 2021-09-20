using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Task5.Models;

namespace Task5.Pages
{
    public class ChoisePageModel : PageModel
    {

        private GameContext GameDb;

        public List<GameModel> Games { get; set; }

        public SelectList Rules { get; set; }

        public ChoisePageModel(GameContext GameContext)
        {
            GameDb = GameContext;
          
        }

        string SessionId;
        public IActionResult OnPost(string Act, string ChoiseRules, int ConnectId)
        {
            Games = GameDb.Games.Where(x => x.GameCompanionId == null).ToList();
            Rules = new SelectList(new List<string>() { "3 хода", "5 ходов", "7 ходов" });
            string SessionId = HttpContext.Session.Id;
            Debug.WriteLine(ChoiseRules);
            ViewData["Name"] = HttpContext.Session.GetString("UserName");
            Debug.WriteLine(Act + SessionId);
            if (Act == "JoinGame")
            {
              Debug.WriteLine(GameDb.Games.Where(x => x.id == ConnectId).FirstOrDefault().GameCompanion);
              if (GameDb.Games.Where(x => x.id == ConnectId).FirstOrDefault().GameCompanion == null)
              { 
                  GameModel p1 = GameDb.Games.Where(x=>x.id == ConnectId).FirstOrDefault();
                  p1.GameCompanion = HttpContext.Session.GetString("UserName");
                  p1.GameCompanionId = SessionId;
                  GameDb.SaveChanges();
                  return Redirect($"/GamePage");
              }
              else
              {
                    return Redirect("/ChoisePage");
              }
            }
            else if (Act == "AddGame")
            {
                GameDb.Add(new GameModel { GameCreator = HttpContext.Session.GetString("UserName"), Unique = SessionId, Rules = ChoiseRules });
                GameDb.SaveChanges();
                int newId = GameDb.Games.Where(x => x.Unique == HttpContext.Session.Id).FirstOrDefault().id;

                return Redirect($"/GamePage");
            }
            else if (Act == "Rename")
            {
                return Redirect("/Index");
            }
            return Page();
        }


        public IActionResult OnGet(string ChoiseRules)
        {
            Games = GameDb.Games.Where(x=>x.GameCompanionId == null).ToList();

            Rules = new SelectList(new List<string>() { "3 хода", "5 ходов", "7 ходов" });
            SessionId = HttpContext.Session.Id;
            Debug.WriteLine(SessionId);
            if (SessionId == GameDb.Games.Where(x => x.Unique == SessionId)?.FirstOrDefault()?.Unique)
            {
                GameModel Delete = GameDb.Games.Where(x => x.Unique == SessionId).FirstOrDefault();
                GameDb.Remove(Delete);
                GameDb.SaveChanges();
            }

            ViewData["Name"] = HttpContext.Session.GetString("UserName");
            return Page();
        }
    }
}
