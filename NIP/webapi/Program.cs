using Microsoft.EntityFrameworkCore;
using webapi.helpers;
using Nip.Models;
using Nip.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
  });

builder.Services.AddDbContext<NipContext>(
#if DEVIL
  option => option.UseMySQL(builder.Configuration.GetConnectionString("MyDevilConnectionString"))
#endif
#if Desktop
  option => option.UseSqlServer(builder.Configuration.GetConnectionString("DesktopConnectionString"))
# endif
);

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
