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
        private DataClass _db;
        public string[] rowsTest = new string[3];
        public string rows = "";

        public void OnGet()
        {
            _db = new DataClass();
            _db.ConfigTables();
            rowsTest = _db.SelectQuery();
            rows = String.Join(" ", rowsTest);
            
        }

        
    }
}