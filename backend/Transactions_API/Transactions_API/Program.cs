using Microsoft.EntityFrameworkCore;
using Transactions_API.Data;
using Transactions_API.Repositories;
using Transactions_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddDbContext<TransactionsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransactionsDb")));
builder.Services.AddHttpClient("ProductsApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7159"); // Products API
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

app.MapControllers();

app.Run();
