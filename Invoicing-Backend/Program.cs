using Invoicing_Backend.Configuration;
using Invoicing_Backend.Data;
using Invoicing_Backend.Helpers;
using Invoicing_Backend.Repositories;
using Invoicing_Backend.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Invoicing_Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddDbContext<InvoicingAppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddRepositories();
        builder.Services.AddScoped<IApplicationService, ApplicationService>();
        builder.Services.AddAutoMapper(typeof(MapperConfig));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.MapControllers();
        
        app.Run();
    }
}