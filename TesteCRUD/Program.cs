
using Regra.Regra;
using Regra.Interfaces;
using ScopedInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<RegraPessoa>();
builder.Services.AddScoped<RegraEndereco>();

Scopedinjection.ConfigurarScoped(services);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
