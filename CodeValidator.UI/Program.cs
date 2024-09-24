using CodeValidator.BLL.AutoMapper;
using CodeValidator.BLL.Service;
using CodeValidator.DAL.Context;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MangoDbService>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
//{
//    microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"]; // clientID need to give we need to set in json settings
//    microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"]; // ClientSeceretKey needed
//});

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
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
