using FinanceWalletIOAPI.Data;
using FinanceWalletIOAPI.DTOs.Mappers;
using FinanceWalletIOAPI.IRepositories;
using FinanceWalletIOAPI.IServices;
using FinanceWalletIOAPI.Models;
using FinanceWalletIOAPI.Repositories;
using FinanceWalletIOAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Common Servies and Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>(); 
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IResponseService, ResponseService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
// Income Module
builder.Services.AddScoped<IncomeSourceDtoMapper>();
builder.Services.AddScoped<IIncomeSourceRepository, IncomeSourceRepository>();
builder.Services.AddScoped<IncomeTransactionDtoMapper>();
builder.Services.AddScoped<IIncomeTransactionRepository, IncomeTransactionRepository>();
// Expense Module
builder.Services.AddScoped<ExpenseSourceDtoMapper>();
builder.Services.AddScoped<IExpenseSourceRepository, ExpenseSourceRepository>();
builder.Services.AddScoped<ExpenseTransactionDtoMapper>();
builder.Services.AddScoped<IExpenseTransactionRepository, ExpenseTransactionRepository>();



builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // used to authenticate the user.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // used to repond for unauthorize user.
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // default scheme for authentication and challenge.
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                                    .GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuerSigningKey = true,

        ValidateLifetime = true, // validate the token expiration time.
        ClockSkew = TimeSpan.Zero // Ensures expiry is exact with no grace period (optional)
    };
});


builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Finance Wallet IO Web API v1");
    });
}
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();