using PurpleBuzzPr.DAL;

namespace PurpleBuzzPr.Services
{
    public class LayoutService
    {
        AppDbContext _dbContext;

        public LayoutService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Dictionary<string, string> GetSettings()
        {
            return _dbContext.Settings.ToDictionary(s=>s.Key, s=>s.Value);
        }
    }
}
