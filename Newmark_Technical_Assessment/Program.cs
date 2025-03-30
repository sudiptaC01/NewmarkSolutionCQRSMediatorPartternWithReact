using MediatR;
using Microsoft.Extensions.Options;
using Newmark_Technical_Assessment.ErrorHandler;
using Newmark_Technical_Assessment.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register service for app setting values and inject them using constructors
builder.Services.Configure<BlobDetailsAppSettings>(builder.Configuration.GetSection("BlobDetails"));

//builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));

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

app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
