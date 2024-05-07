using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ECommerceWeb.Data;
using ECommerceWeb.Models;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ECommerceWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceWebContext") ?? throw new InvalidOperationException("Connection string 'ECommerceWebContext' not found.")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(

    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 8;
    }    
    
    
).AddEntityFrameworkStores<ECommerceWebContext>().AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
