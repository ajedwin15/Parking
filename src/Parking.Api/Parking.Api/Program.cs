using System.Text.Json.Serialization;
using DB;
using Microsoft.EntityFrameworkCore;
using src.Parking.Api.Parking.Api.Repository;
using src.Parking.Api.Parking.Api.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//sigleton for settings
builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            );

builder.Services.AddDbContext<ParkingContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ParkingConection"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ParkingContex>();
    context.Database.Migrate();
}

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
