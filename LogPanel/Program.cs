using LogPanel.Models;
using LogPanelEntities;
using LogPanelEntities.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddClientLogConfig(c =>
{
    c.Add(new CustomClient()
    {
        Name = "Logger",
        Database = new Database(
            name: "LoggerDb",
            server: "(localdb)\\MSSQLLocalDB",
            logTable: "LogExcepciones",
            columnConfig: x =>
            {
                x.ColNameForId = "Id";
                x.ColNameForLogType = "LogType";
                x.ColNameForTime = "FechaGeneracion";
                x.ColNameForMessage = "Message";
                x.ColNameForStacktrace = "StackTrace";
            }),
        Description = "3.0",
        CustomDescription = "This is a custom description"
    });

    c.Add(new Client()
    {
        Name = "Academia dotNet",
        Database = new Database(
            name: "AcademiaDotNetDb",
            server: "(localdb)\\MSSQLLocalDB",
            logTable: "LogExcepciones",
            columnConfig: x =>
            {
                x.ColNameForId = "Id";
                x.ColNameForLogType = "";
                x.ColNameForTime = "FechaGeneracion";
                x.ColNameForMessage = "Message";
                x.ColNameForStacktrace = "StackTrace";
            }),
        Description = "1.0"
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
