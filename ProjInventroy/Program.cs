using Microsoft.EntityFrameworkCore;
using ProjInventroy.Hubs;
using ProjInventroy.Models;
using ProjInventroy.Services.Email;
using ProjInventroy.Services.Inventroy;

namespace ProjInventroy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IInventroyService, InventroyService>();
            builder.Services.AddScoped<InventoryService>();

            builder.Services.AddSignalR();



            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var provider = builder.Services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            builder.Services.AddDbContext<eCommerceContext>(item => item.UseSqlServer
            (config.GetConnectionString("conn")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Index}/{id?}");

            app.MapHub<InventoryHub>("/inventoryHub");

            app.Run();
        }
    }
}
