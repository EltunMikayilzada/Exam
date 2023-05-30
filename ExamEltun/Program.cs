using ExamEltun.DAL;
using ExamEltun.Models;
using ExamEltun.service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddDbContext<AppDbcontext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<AppUser,IdentityRole>(opt=>
{ 
    opt.Password.RequireNonAlphanumeric=false;
    opt.Password.RequiredLength = 8;
    
}).AddEntityFrameworkStores<AppDbcontext>().AddDefaultTokenProviders();
var app = builder.Build();


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name:"Default",
    pattern:"{controller=home}/{action=index}/{id?}"
    );

app.Run();
