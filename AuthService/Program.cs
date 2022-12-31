using AuthService.Entities;
using AuthService.Handler;
using AuthService.Helper;
using AuthService.Services;
using rabbitMessaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// configure strongly typed settings object
builder.Services.Configure<UserDatabaseSettings>(
builder.Configuration.GetSection("UserDatabase"));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("JWTSecret"));

builder.Services.AddTransient<IMessageHandler>((svc) =>
{
    var rabbitMQConfigSection = builder.Configuration.GetSection("RabbitMQ");
    string rabbitMQHost = rabbitMQConfigSection["Host"];
    string rabbitMQUserName = rabbitMQConfigSection["UserName"];
    string rabbitMQPassword = rabbitMQConfigSection["Password"];
    return new RabbitMQMessageHandler(rabbitMQHost, rabbitMQUserName, rabbitMQPassword, "studentExchange", "Users", ""); 
});


builder.Services.AddHostedService<UserHandler>();
// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
