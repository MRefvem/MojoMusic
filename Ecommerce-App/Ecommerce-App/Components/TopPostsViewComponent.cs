using Ecommerce_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Components
{
    [ViewComponent]
    public class TopPostsViewComponent : ViewComponent
    {
        public TopPostsViewComponent()
        {

        }

        // make actions
        //public async Task<IViewComponentResult> InvokeAsync(int number)
        //{
        //    // do some logic to pull from the DB 
            
        //    var posts = sqlite3_context.Posts
        //    return View();
        //}
    }
}
