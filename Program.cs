var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.WebHost.UseUrls("http://localhost:5001",
    "http://172.27.80.1:5001", 
    "http://10.0.0.25:5001");

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

//app.UseHttpsRedirection();10.0.0.25

app.UseAuthorization();

app.MapControllers();

app.Run();
