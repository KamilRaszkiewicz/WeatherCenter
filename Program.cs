using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherCenter.Interfaces;
using WeatherCenter.Models;
using WeatherCenter.Services.Apis;

namespace WeatherCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("secrets.json");

            builder.Services.AddRazorPages();
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
        

            builder.Services.AddSingleton<IAutocompleteService, GooglePlacesAutocompleteService>();
            builder.Services.AddSingleton<IWeatherService, WeatherapiWeatherService>();

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

            app.MapGet("/autocomplete", async (IAutocompleteService svc, [FromQuery(Name = "q")] string query) => await svc.GetAutocompletes(query));
            app.MapGet("/coordinates", async (IAutocompleteService svc, [FromQuery(Name = "q")] string placeId) => await svc.GetCoordinates(placeId));
            app.MapGet("/weather/{latitude},{longtitude}", async (IWeatherService svc, double latitude, double longtitude) => await svc.GetCurrentWeather(latitude, longtitude));

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}