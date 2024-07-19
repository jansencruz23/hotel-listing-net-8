using HotelListing.MVC.Contracts;
using HotelListing.MVC.Services;
using HotelListing.MVC.Services.Base;
using HotelListing.MVC.Services.Identity;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient<IClient, Client>(client =>
    client.BaseAddress = new Uri("https://localhost:7025"));

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

//builder.Services.AddHttpCacheHeaders(
//    (expirationOptions) =>
//    {
//        expirationOptions.MaxAge = 120;
//        expirationOptions.SharedMaxAge = 60;
//        expirationOptions.CacheLocation = CacheLocation.Private;
//    },
//    (validationOptions) =>
//    {
//        validationOptions.MustRevalidate = true;
//        validationOptions.ProxyRevalidate = true;
//    }
//);

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

//app.UseHttpCacheHeaders();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
