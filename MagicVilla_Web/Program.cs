using MagicVilla_Web;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AutoMapper Injection
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Register Http Client to Villa Service
builder.Services.AddHttpClient<IVillaService, VillaService>();
//Register VillaService (dependency injection)
//Scoped is using same object for a lifetime
builder.Services.AddScoped<IVillaService, VillaService>();

//Register VillaNumberService (dependency injection)
builder.Services.AddHttpClient<IVillaNumberService, VillaNumberService>();
builder.Services.AddScoped<IVillaNumberService, VillaNumberService>();

//Register AuthService (dependency Injection)
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Preserve login token in a session variable on application
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    //
    option.IdleTimeout = TimeSpan.FromMinutes(100);
    //
    option.Cookie.HttpOnly = true;
    //
    option.Cookie.IsEssential = true;
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
//token session
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
