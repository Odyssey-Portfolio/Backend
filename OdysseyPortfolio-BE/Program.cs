using OdysseyPortfolio_BE.Extensions;
using OdysseyPortfolio_Libraries.Constants;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddCorsConfig();
builder.Services.AddSecurity(config);
builder.Services.AddUnitOfWork();
builder.Services.AddServices();
builder.Services.AddSwaggerConfig();
builder.Services.AddDatabase(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(ServiceExtensionsConstants.ODYSSEY_PORTFOLIO_DEPLOYMENT_CORS);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.Services.InitializeSecurityAsync();
await app.Services.SeedRootAdminAsync();
app.Run();
