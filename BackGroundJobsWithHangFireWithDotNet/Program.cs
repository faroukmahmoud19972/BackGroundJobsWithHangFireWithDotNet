using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(x => x.UseSqlServerStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HangFire;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();
app.UseHangfireDashboard("/dashboard");

app.MapControllers();

app.Run();
