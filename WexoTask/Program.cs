using DAO;
using Microsoft.Extensions.Options;
using WexoTask.APIConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    return new GenreAPIConsumer(settings.BaseUrl, settings.BearerToken);

});

builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    return new MovieAPIConsumer(settings.BaseUrl, settings.BearerToken);

});

builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    return new CreditsAPIConsumer(settings.BaseUrl, settings.BearerToken);

});

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
