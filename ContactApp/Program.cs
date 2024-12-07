using ContactApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Bağlantı sətirini oxuyun
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Servisi DI konteynerinə əlavə edin
builder.Services.AddScoped<ContactService>(_ => new ContactService(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contact}/{action=Index}/{id?}");
app.Run();
