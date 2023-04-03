using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
// set culture
builder.Services.AddLocalization();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Home/";
    });

//builder.Services.AddSession(); //Registered Session
builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

builder.Services.AddHttpContextAccessor(); //registered HttpContextAccessor

//builder.Services.AddAuthorization();

var app = builder.Build();
//set culture

//var culture = new CultureInfo("en-ID");

//var supportedCultures = new[]
//{
//    culture
//};
//app.UseRequestLocalization(new RequestLocalizationOptions
//{
//    DefaultRequestCulture = new RequestCulture(culture),
//    SupportedCultures = supportedCultures,
//    SupportedUICultures = supportedCultures,
//    FallBackToParentCultures = false
//});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot");
app.UseRouting();

app.UseAuthentication(); // add benny
//app.UseAuthorization();
app.UseSession(); //registered Session

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();