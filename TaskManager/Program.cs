using TaskManager.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOcelot();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskDbContext>(options => options.UseInMemoryDatabase("TaskDatabase"));
var app = builder.Build();
//app.UseSwaggerForOcelotUI(opt =>
//{
//    opt.PathToSwaggerGenerator = "/ocelot/swagger/docs/v1";
//});
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); 
startup.Configure(app, builder.Environment);