using BackgroundJobsWithHangfire.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


//Database Context
builder.Services.AddDbContext<BackgroundJobsWithHangfire.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
//Hangfire

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(
            builder.Configuration.GetConnectionString("DefaultConnection")
    );
});
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IJobTestService, JobTestService>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseHttpsRedirection();

app.UseAuthorization();

// Hangfire Dashboard
app.UseHangfireDashboard();
//app.UseHangfireDashboard("/hangfire", new DashboardOptions());


app.MapControllers();

app.Run();
