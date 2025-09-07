 
using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using AbySalto.Junior.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AbySalto.Junior
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurant", Version = "v1" }); });
            builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddCors(options => { options.AddPolicy("AllowAll", builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }); });
            var app = builder.Build();
            Initialize_Database_Asynchronously(app);
            if(app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); options.RoutePrefix = string.Empty; options.DefaultModelsExpandDepth(-1);  });
            }
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static async Task Initialize_Database_Asynchronously(WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreatedAsync();
            }
        }
    }
}
