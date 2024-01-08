using Microsoft.EntityFrameworkCore;
using NZwalker.Data;
using NZwalker.Models.AutoMapper;
using NZwalker.Repositories.IRepo;
using NZwalker.Repositories.SQLRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))
);

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddScoped<IWalksRepository, SQLWalksRepository>();

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
