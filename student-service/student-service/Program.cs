using Microsoft.AspNetCore.Authentication.JwtBearer;
using rabbitMessaging;
using student_service.Models;
using student_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<StudentDatabaseSettings>(
builder.Configuration.GetSection("StudentDatabase"));
var configSection = builder.Configuration.GetSection("RabbitMQ");
builder.Services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(configSection["Host"],
    configSection["UserName"], configSection["Password"], "studentExchange", "fanout"));
builder.Services.AddScoped<StudentsService>();
builder.Services.AddScoped<StandardService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
