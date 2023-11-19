using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Repository.Interfaces;
using RestaurantApi.Repository.Implementations;
using Microsoft.AspNetCore.Identity;
using RestaurantApi.Models.IdentityContext;

namespace RestaurantApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(opts =>            
                opts.UseSqlServer(builder.Configuration["ConnectionStrings:RestaurantConnection"]));

            builder.Services.AddDbContext<IdentityContext>(opts =>
                opts.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"]));

            builder.Services.AddResponseCaching();
            builder.Services.AddMemoryCache();
            builder.Services.AddIdentity<RestaurantUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<IdentityContext>();                

            builder.Services.AddScoped<IDishRepository, EfDishRepository>();
            builder.Services.AddScoped<IDishTypesRepository, EfDishTypesRepository>();
            builder.Services.AddScoped<IImageRepository, EfImageRepository>();           

            builder.Services.AddControllers()
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", builder =>
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

            var app = builder.Build();

            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantApi");
            });

            if (app.Environment.IsDevelopment())
            {               
                SeedData.SeedDatabase(app);
                SeedIdentity.SeedDatabase(app, builder.Configuration);
            }

            app.Run();
        }
    }
}