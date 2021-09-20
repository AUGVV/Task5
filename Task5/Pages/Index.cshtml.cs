using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private string name;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger; 
        }

  
            public IActionResult OnGet(string UserName = "")
            {
               if(UserName != null && UserName.Length != 0)
               {
                  HttpContext.Session.SetString("UserName", UserName);
                  return Redirect("/ChoisePage");
               }
               return Page();
            }
    }
}
