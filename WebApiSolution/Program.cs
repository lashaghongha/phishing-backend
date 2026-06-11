using Data;
using Microsoft.EntityFrameworkCore;
using Services;

namespace WebApiSolution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            string connectionString;
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                // Railway format: postgresql://user:pass@host:port/db
                var uri = new Uri(databaseUrl);
                var userInfo = uri.UserInfo.Split(':');
                connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
            }
            else
            {
                connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
            }

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("WebApiSolution")));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<DbContext, AppDbContext>();
            builder.Services.AddTransient<IProductService, ProductService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
