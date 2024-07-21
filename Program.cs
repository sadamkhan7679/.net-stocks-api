using api.Data;
using api.Interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register NewtonsoftJson. This is the dependency injection container that will be used to inject the NewtonsoftJson.
// This is used to ignore reference loops in the JSON serialization
builder.Services.AddControllers().AddNewtonsoftJson((options) =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Register the ApplicationDbContext. This is the dependency injection container that will be used to inject the ApplicationDbContext into the StockController
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


// Register the StockRepository, IStockRepository. This is the dependency injection container that will be used to inject the StockRepository into the StockController
builder.Services.AddScoped<IStockRepository, StockRepository>();

// Register the CommentRepository, ICommentRepository. This is the dependency injection container that will be used to inject the CommentRepository into the CommentController
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();