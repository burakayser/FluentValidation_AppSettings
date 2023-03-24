using FluentValidation;
using FluentValidation_AppSettings.Infrastructure.Extensions;
using FluentValidation_AppSettings.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

builder.Services.AddOptions<ExampleOptions>()
    .Bind(builder.Configuration.GetSection(ExampleOptions.SectionName))
    .ValidateFluently()
    .ValidateOnStart();


// Add services to the container.
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


app.Run();

