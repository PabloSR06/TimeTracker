using MySqlConnector;
using timeTrakerApi.Data;
using timeTrakerApi.Data.Interface;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);
            builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
            builder.Services.AddTransient<IDayHoursRepository, DayHoursRepository>();
            builder.Services.AddTransient<IClientRepository, ClientRepository>();
            builder.Services.AddTransient<IProjectHoursRepository, ProjectHoursRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddCors(options =>
                        {
                            options.AddPolicy("AllowOrigin", builder =>
                                builder.WithOrigins("http://localhost:3000")
                                       .AllowAnyMethod()
                                       .AllowAnyHeader());
                        });

            var app = builder.Build();




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowOrigin");
            app.UseRouting();


            app.MapControllers();

            app.Run();
        }
    }
}
