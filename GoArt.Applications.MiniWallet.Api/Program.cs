using FluentValidation;
using GoArt.Applications.MiniWallet.Api.Middleware;
using GoArt.Applications.MiniWallet.Core.Data;
using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Localization;
using GoArt.Applications.MiniWallet.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DapperContext>();

string? connectionString = builder.Configuration.GetConnectionString("GoArtConnection");
ArgumentException.ThrowIfNullOrEmpty(connectionString);

builder.Services.AddMediatR((configuration) =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Wallet).Assembly);
});

builder.Services.AddMemoryCache();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<ILocalizer, DefaultLocalizer>();
builder.Services.AddTransient<IWalletRepository, DapperWalletRepository>();
builder.Services.AddTransient<IMoneyTransactionLogRepository, DapperMoneyTransactionLogRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<Wallet>();
builder.Services.AddTransient<ICurrencyConverter, DefaultCurrencyConverter>();

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
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();