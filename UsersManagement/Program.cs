using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UsersManagement.BAL.Services;
using UsersManagement.DAL;
using UsersManagement.DAL.Repositories;
using UsersManagement.DAL.Repositories.Interfaces;
using UsersManagement.RouteGroups;

var baseUrl = "/users-management";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
});

builder.Services.AddTransient<ISubscriptionsRepository, SubscriptionsRepository>();
builder.Services.AddTransient<SubscriptionsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup($"{baseUrl}/subscriptions")
    .MapSubscriptionsApi()
    .WithTags("Subscriptions");

app.Run();