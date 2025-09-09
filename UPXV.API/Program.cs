using UPXV.Data;
using UPXV_API;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddLogging();

builder.Services.AddDependencies();

builder.Services.AddDbContext(config);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(c => {
   c.SwaggerEndpoint("swagger", "Swagger");
});

//app.UseAuthorization();

app.MapControllers();

app.Run();
