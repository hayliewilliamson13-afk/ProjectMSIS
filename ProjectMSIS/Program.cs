using Mindscape.Raygun4Net.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

// Add services
builder.Services.AddControllersWithViews(); // or whatever you already have

// Add Raygun — reads ApiKey from appsettings.json
builder.Services.AddRaygun(builder.Configuration);

// Put these BEFORE UseRaygun
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

// Raygun goes AFTER exception handlers
app.UseRaygun();

app.Run();

app.MapGet("/throw", (Func<string>)(() => throw new Exception("Exception in request pipeline")));

