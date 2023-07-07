using Project3.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

//Conecting Interfaces with implemantations to be used
builder.Services.AddSingleton<IContactRepository, DbContactRepository>();
builder.Services.AddSingleton<ICategoryRepository, DbCategoryRepository>();
builder.Services.AddSingleton<ISubCategoryRepository, DbSubCategoryRepository>();
builder.Services.AddSingleton<ILoginRepository, DbLoginRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
