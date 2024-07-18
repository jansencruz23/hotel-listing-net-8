using HotelListing.MVC.Contracts;
using HotelListing.MVC.Services;
using HotelListing.MVC.Services.Base;
using HotelListing.MVC.Services.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient<IClient, Client>(client =>
    client.BaseAddress = new Uri("http://localhost:90/hotellisting_api/"));

builder.Services.Configure<CookiePolicyOptions>(options =>
    options.MinimumSameSitePolicy = SameSiteMode.None);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddTransient<IClientAuthenticationService, ClientAuthenticationService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/User/Login";
    });

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IHotelService, HotelService>();

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
