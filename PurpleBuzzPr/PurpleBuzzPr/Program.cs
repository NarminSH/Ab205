using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"))
);

var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.MapControllerRoute(
    name: "singlework",
    pattern: "{controller=SingleWork}/{action=Index}/{id?}"
);

app.Run();