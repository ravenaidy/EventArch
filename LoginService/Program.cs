using FluentValidation;
using Infrastructure.AutoMapperExtensions;
using Infrastructure.Connection;
using Infrastructure.Connection.Contracts;
using LoginService.Commands;
using LoginService.Models;
using LoginService.PipelineBehaviors;
using LoginService.Repositories;
using LoginService.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionFactory>(sp =>
    new ConnectionFactory<SqlConnection>(builder.Configuration.GetConnectionString("Login")));
builder.Services.AddSingleton<ILoginRepository, LoginRepository>();
builder.Services.AddObjectMapping(typeof(Login).Assembly);

// Add Mediator and PipeLines
builder.Services.AddMediatR(typeof(RegisterLoginCommand));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(LoginService.Validations.RegisterLoginValidation).Assembly);

var app = builder.Build();

app.Run();