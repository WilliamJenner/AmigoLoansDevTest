using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SQLite;

namespace AmigoLoansDevTest.Pages
{
    public class IndexModel : PageModel
    {
        private Logic logic = new Logic();
        public string[] engineers;

        public void OnGet()
        {
            engineers = logic.SelectEngineers();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }

    }
}