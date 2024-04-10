using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using World.API.Common;
using World.API.Data;
using World.API.Repository;
using World.API.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomPolicy",x=>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())   ;
});
#endregion

#region Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WorldAPIDbContext>(options => options.UseSqlServer(connectionString));
#endregion

#region Configure Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region ICountryRepository
builder.Services.AddTransient<ICountryRepository,CountryRepository>();
#endregion

#region IStateRepository
builder.Services.AddTransient<IStateRepository,StateRepository>();
#endregion

#region Generic Reposistory
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
#endregion

#region Configure Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);
    if(context.HostingEnvironment.IsProduction() == false)
    {
        config.WriteTo.Console();
    }
});
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
