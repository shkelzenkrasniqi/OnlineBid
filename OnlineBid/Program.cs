using API.Extensions;
using Application.Configuration;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using OnlineBid.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddFluentValidationAutoValidation();

RepositoriesDIConfiguration.Configure(builder.Services);
ServicesDIConfiguration.Configure(builder.Services, builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityServiceJWT(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", builder =>
    {
        builder
            .WithOrigins("http://localhost:4200")  
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapHub<BidHub>("/bidhub");
app.UseCors("AngularApp");
app.Run();
