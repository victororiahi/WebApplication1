using WebApplication1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//PART 1
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//PART 2
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<DataSyncMiddleware>();
app.MapControllers();

//PART 3
app.Run();
