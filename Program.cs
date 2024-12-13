using System.Net.Sockets;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


// Add services to the container.
var localUrl = "";
var host = Dns.GetHostEntry(Dns.GetHostName());

foreach (var ip in host.AddressList)
{
    if (ip.AddressFamily == AddressFamily.InterNetwork)
    {
        localUrl = ip.ToString();
    }
}

builder.WebHost.UseUrls("http://localhost:5001", "http://" + localUrl + ":5001");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors((builder) => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();//10.0.0.25

app.UseAuthorization();

app.MapControllers();

app.Run();
