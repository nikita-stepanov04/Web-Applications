using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Repository.Interfaces;
using RestaurantApi.Repository.Implementations;

namespace RestaurantApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration["ConnectionStrings:RestaurantConnection"]);
                opts.EnableSensitiveDataLogging();
            });

            builder.Services.AddControllers()
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("Default", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RestaurantApi",
                    Version = "v1"
                });
            });

            builder.Services.AddResponseCaching();
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<IDishRepository, EfDishRepository>();
            builder.Services.AddScoped<IDishTypesRepository, EfDishTypesRepository>();
            builder.Services.AddScoped<IImageRepository, EfImageRepository>();

            var app = builder.Build();

            app.UseResponseCaching();

            app.MapControllers();

            app.UseCors("Default");

            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantApi");
            });

            if (app.Environment.IsDevelopment())
            {
                DataContext context = app.Services.CreateScope()
                    .ServiceProvider.GetRequiredService<DataContext>();
                SeedData.SeedDatabase(context);
            }

            app.Run();
        }
    }
}