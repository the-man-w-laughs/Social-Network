using SocialNetwork.DAL;
using SocialNetwork.BLL;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.RegisterDalDependencies(builder.Configuration);
builder.Services.RegisterBllDependencies(builder.Configuration);

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

using (var db = new SocialNetworkContext())
{
}

app.Run();
