using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrapuncle.Data;
using Scrapuncle.Hubs;
using Scrapuncle.Models;
using Scrapuncle.Models.Interface;
using Scrapuncle.Models.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// dependency injection
//builder.Services.TryAddScoped<IPickupRepository, PickupRepository>();
builder.Services.AddScoped<IPickupRepository, PickupRepository>();

builder.Services.AddSignalR();



builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("LahoreCityAccess", policy =>
    //     policy.RequireClaim("Location", "lahore"));

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("Role", "Admin"));

    options.AddPolicy("DealerOnly", policy =>
        policy.RequireClaim("Role", "Dealer"));

    options.AddPolicy("UserOnly", policy =>
        policy.RequireClaim("Role", "User"));


    options.AddPolicy("BusinessHoursOnly", policy =>
        policy.RequireAssertion(context =>
            DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 23));

    
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.MapHub<ChatHub>("/chatHub");

app.Run();
