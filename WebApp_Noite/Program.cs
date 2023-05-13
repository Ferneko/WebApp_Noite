using Microsoft.EntityFrameworkCore;
using WebApp_Noite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Contexto>(
    //opt => opt.UseSqlServer("Server=c3po;Database=pw_noite_Ferneko;Trusted_Connection=True;")
    opt => opt.UseSqlServer("Server=c3po;Database=pw_noite_Ferneko;User Id=sa;Password=aluno123;")
    );



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
