using OdysseyPortfolio_BE;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddCorsConfig();
builder.Services.AddSecurity();
builder.Services.AddUnitOfWork();
builder.Services.AddServices();
builder.Services.AddSwaggerConfig();
builder.Services.AddDatabase();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("OdysseyPortfolioLocal");

app.Run();
