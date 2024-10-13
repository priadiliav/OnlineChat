
using Chat.Server.Contracts;
using Chat.Server.Hubs;
using Chat.Server.Repositories.Redis;
using Chat.Server.Services;
using StackExchange.Redis;

namespace Chat.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddControllers();
                builder.Services.AddSignalR();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder
                            .WithOrigins("https://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
                });

                var redis = ConnectionMultiplexer.Connect("localhost:6379,password=yourpassword");
                builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

                builder.Services.AddSingleton<IUserContract, RedisUserRepository>();
                builder.Services.AddSingleton<IMessageContract, RedisMessageRepository>();

                builder.Services.AddScoped<UserService>(); 
                builder.Services.AddScoped<MessageService>();
            }

            var app = builder.Build();
            {
                app.UseDefaultFiles();
                app.UseStaticFiles();
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.UseCors("AllowSpecificOrigin");

                app.MapHub<ChatHub>("/chat");

                app.MapControllers();
            }
            app.Run();
        }
    }
}
