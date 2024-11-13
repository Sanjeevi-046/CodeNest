// ***********************************************************************************************
//
//  (c) Copyright 2024, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: CodeNest .
//
// ***********************************************************************************************

using CodeNest.BLL.AutoMapper;
using CodeNest.BLL.Service;
using CodeNest.DAL.Context;
using CodeNest.DAL.Repository;
using CodeNest.UI.tempdata;
using Microsoft.AspNetCore.Authentication.Cookies;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IFormatterServices, FormatterServices>();
builder.Services.AddTransient<IWorkspaceService, WorkspaceService>();
builder.Services.AddTransient<IWorkSpaceRepository, WorkSpaceRepository>();
builder.Services.AddTransient<IJsonService, JsonService>();
builder.Services.AddTransient<IJsonRepository, JsonRepository>();
builder.Services.AddTransient<IFormatterRepository, FormatterRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("", _client =>
{
    _client.Timeout = TimeSpan.FromMinutes(5);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.Name = "CodeNest";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
    });
    builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout period
    options.Cookie.HttpOnly = true; // Cookie settings
    options.Cookie.IsEssential = true;
});
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}
app.UseAuthentication();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.UseMiddleware<TempDataMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
