using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Elasticsearch configuration
var settings = new ConnectionSettings(new Uri("http://localhost:9200")) // Replace with your Elasticsearch instance URL
    .DefaultIndex("products"); // Set a default index name

var client = new ElasticClient(settings);

// Registering Elasticsearch client as a singleton service
builder.Services.AddSingleton<IElasticClient>(client);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();