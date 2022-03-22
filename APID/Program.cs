using APID.Helpers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

///  Configuracion para Heroku

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";


     

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AplicationDbContext>(options => 
                         options.UseMySql(connectionString,
                         ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors();


builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();



// Aplicar las nuevas migraciones al ejecutar la aplicacion y alimentar la Base de datos
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
         var context = services.GetRequiredService<AplicationDbContext>();
         await context.Database.MigrateAsync();
        // await BaseDatosSeed.SeedAsync(context, loggerFactory);
    }
    catch (System.Exception ex)
    {
        
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Un error ocurrio durante la migracion" );
    }

}
//////////////////////////////////

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }


app.UseSwagger();
app.UseSwaggerUI();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());

app.UseStaticFiles();


app.UseAuthorization();

app.MapControllers();

app.Run();
