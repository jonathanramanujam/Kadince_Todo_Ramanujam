using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kadince_Todo_Ramanujam.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Kadince_Todo_RamanujamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Kadince_Todo_RamanujamContext") ?? throw new InvalidOperationException("Connection string 'Kadince_Todo_RamanujamContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
