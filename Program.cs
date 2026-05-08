using EventEase.Data;
using EventEase.Services;
using Microsoft.EntityFrameworkCore;
using EventEase.Services.Interfaces;
using EventEase.Services.Blob;

var builder = WebApplication.CreateBuilder(args);

bool useAzureBlob = false;

builder.Services.AddControllersWithViews();

if (useAzureBlob)
{
    builder.Services.AddScoped<IFileUploadService, AzureBlobStorageService>();
}
else
{
    builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IBookingService, BookingService>();

/*
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
*/
builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();