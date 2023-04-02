using HostedSupervizer.Constants;
using HostedSupervizer.Services;
using HostedSupervizer.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<APIHostSettings>(builder.Configuration.GetSection(SettingsConstants.AdminAPIHost));

builder.Services.AddSingleton<IAPIHostSettings>(options =>
    options.GetRequiredService<IOptions<APIHostSettings>>().Value);
builder.Services.AddSingleton<Supervizer>();
builder.Services.AddHostedService(options => options.GetService<Supervizer>());

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
