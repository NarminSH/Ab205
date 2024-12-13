using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.Abstractions;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;
using PurpleBuzzPr.Services;
using PurpleBuzzPr.Utilities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 4;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredUniqueChars = 2;
    opt.User.RequireUniqueEmail = true;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<AppDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"))
);
builder.Services.AddScoped<LayoutService>();

var app = builder.Build();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/GlobalException");
//}


app.UseSession();


app.UseStatusCodePagesWithReExecute("/Home/Error");

//app.UseStatusCodePages(contentType:"text/html", bodyFormat:"<h1>404 not found</h1>");


app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "singlework",
    pattern: "{controller=SingleWork}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);





app.Run();
