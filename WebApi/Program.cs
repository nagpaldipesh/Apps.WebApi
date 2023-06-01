using Webapi.Data.Repositories;
using Webapi.Data.Repositories.Interfaces;
using WebApi.DbEntities;
using WebApi.Services;
using WebApi.Services.Interfaces;
using Attribute = WebApi.DbEntities.Attribute;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAttributeService, AttributeService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IGenericRepository<Page>, GenericRepository<Page>>();
builder.Services.AddScoped<IGenericRepository<Attribute>, GenericRepository<Attribute>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
