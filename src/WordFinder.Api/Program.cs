using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WordFinder.Api.DependencyInjection;
using WordFinder.Api.Features.FindWords;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy/*.WithOrigins("https://localhost:4200/","https://localhost:3000/api/")*/
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader(); 
        });
});
builder.Services.AddApplicationServices();
builder.Services.Configure<JsonOptions>(options => {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(action =>
    {
        action.DisplayRequestDuration();
        action.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
    });
}
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseRateLimiter();
app.MapFindWordsEndpoints();    

app.Run();
