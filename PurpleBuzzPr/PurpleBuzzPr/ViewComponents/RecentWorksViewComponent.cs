using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;
using System.Collections.Generic;

namespace PurpleBuzzPr.ViewComponents
{
    public class RecentWorksViewComponent : ViewComponent
    {
        AppDbContext _dbContext;

        public RecentWorksViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            IEnumerable<Work> recentWorks = await _dbContext.Works.OrderByDescending(w => w.Id).Take(count).ToListAsync();
            return View(recentWorks);  

        }

    }
}
