using Microsoft.AspNetCore.Authentication.Cookies;
using SocialNetwork.DAL;
using SocialNetwork.API.Middlewares;
using SocialNetwork.API.Extensions;
using SocialNetwork.BLL.Extensions;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Web.Http.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
            .AddFluentValidation();

builder.Services.RegisterDalDependencies(builder.Configuration);
builder.Services.RegisterBllDependencies(builder.Configuration);

builder.Services.AddControllersWithNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithXmlComments();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.Cookie.Name = "AuthCookie";
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();


