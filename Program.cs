using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherCenter.Interfaces;
using WeatherCenter.Models;
using WeatherCenter.Services;
using WeatherCenter.Services.Apis;

namespace WeatherCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("secrets.json");

            builder.Services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            builder.Services.AddHttpClient("weatherapi.com", opts =>
            {
                opts.BaseAddress = new Uri("https://api.weatherapi.com");
            });

            builder.Services.AddHttpClient("maps.googleapis.com", opts =>
                {
                    opts.BaseAddress = new Uri("https://maps.googleapis.com");
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddSingleton<ILocationsService, GooglePlacesService>();
            builder.Services.AddSingleton<IWeatherService, WeatherapiWeatherService>();
            builder.Services.AddScoped<IWidgetService, WidgetService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();

            app.UseRouting();

            app.UseAuthorization();

            app.Run();
        }
    }
}