using Microsoft.AspNetCore.SignalR;
using Sample.SignalR.Proximity.Toaster;
using Samples.Host;
using SignalR.Proximity;
using SignalR.Proximity.Hosting;
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR();//  <--------------
builder.Services.AddSingleton<IUserIdProvider, NoSecureUserIdProvider>();
builder.Services.AddProximity();//  <--------------

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();

app.UseRouting()
    .UseEndpoints(endpoints =>
{
    endpoints.MapProximity<IToastNotificationsContract>();
    endpoints.MapProximity<ISchoolContract>();
});

app.UseAuthorization();

app.MapRazorPages();



app.Run();

