using SocialNetwork.DAL.DataContext;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

using (ApplicationContext db = new ApplicationContext())
{
    User user1 = new User {};
    User user2 = new User { };
    Post post = new Post() {};
    // Добавление\
    db.Posts.Add(post);
    db.Users.Add(user1);
    db.Users.Add(user2);
    db.SaveChanges();
}

app.Run();