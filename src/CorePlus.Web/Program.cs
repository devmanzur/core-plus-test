using CorePlus.Web.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReportModule(builder.Configuration);
builder.Services.AddAppointmentModule(builder.Configuration);
builder.Services.AddHostedService<DatabaseSeedingService>();
builder.Services.AddSpaStaticFiles(options => options.RootPath = "ClientApplication/dist");
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSpa(spa =>
{
    spa.Options.SourcePath = NuxtIntegration.SpaSource;
    if (builder.Environment.IsDevelopment())
    {
        // Launch development server for Nuxt
        spa.UseNuxtDevelopmentServer();
    }
});

app.Run();