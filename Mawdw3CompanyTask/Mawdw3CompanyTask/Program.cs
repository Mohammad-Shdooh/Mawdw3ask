using BAL.Interfaces;
using BAL.Services;
using DAL.Interfaces;
using DAL.Mapper;
using DAL.Repository;
using Entity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// JWT  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // to make a token expired 
        ClockSkew = TimeSpan.Zero, // Set ClockSkew to zero to enforce strict token expiration
        ValidateIssuerSigningKey = true
    };
});






builder.Services.AddMvc();
builder.Services.AddDbContext<Mawdw3TaskContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("mawdw3Task")) 
    );

builder.Services.AddScoped<IStudentsService, StudentsToolsService>();
builder.Services.AddScoped<IStudentsRepository,StudentsTools>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepository,UserRepository>();





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UserProfile)); //for AutoMapping

var _logger = new LoggerConfiguration()
            .WriteTo.File("Log/log-.log", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

builder.Logging.AddSerilog(_logger);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();


app.Run();
