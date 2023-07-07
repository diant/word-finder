using WordFinder.Api.DependencyInjection;
using WordFinder.Api.Features.FindWords;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy/*.WithOrigins("https://localhost:4200/","https://localhost:32768/api/")*/
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader(); 
        });
});

builder.Services.AddWordFinder();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.MapFindWords();

app.Run();
