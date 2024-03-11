using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Registration of MappingConfig
builder.Services.AddAutoMapper(typeof(MappingConfig));
//Registration of VillaRepository
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
//Registration of VillaNumberRepository
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
//Connection String
//builder.Services.AddDbContext<AppDBContext>
//    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));
builder.Services.AddDbContext<AppDBContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureConnectionString")));

builder.Services.AddControllers(options =>
{ //options.ReturnHttpNotAcceptable = true;//If API format is not Json, return error
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();//Accepts Xml format for API

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Custom ILogger dependency injection
builder.Services.AddSingleton<ILogging, LoggingV2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
