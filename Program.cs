using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SmokeTracker.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();
builder.Services.AddDbContext<StContext>(opts => opts.UseInMemoryDatabase("SmokeTracker"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<StContext>();

    var logs = new List<Log>();

    for (int i = 0; i < 10; i++)
    {
        var log = new Log(Guid.NewGuid(), DateTime.Now.AddDays(i));
        logs.Add(log);
    }

    await context.Logs.AddRangeAsync(logs);
    await context.SaveChangesAsync();
}


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
