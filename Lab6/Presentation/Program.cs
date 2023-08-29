using Application.Extensions;
using DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Presentation.Converters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddDataAccess(x => x.UseLazyLoadingProxies().UseSqlite("Data Source=database.db"));

builder.Services.AddControllers().AddNewtonsoftJson(settings =>
{
    // settings.SerializerSettings.Converters.Add(new MessageDtoJsonConverter());
    settings.SerializerSettings.Converters.Add(new CreateMessageSourceModelJsonConverter());
    settings.SerializerSettings.Converters.Add(new CreateMessageModelJsonConverter());

    

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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