using UserApi.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://0.0.0.0:80");


var mongoConnection = builder.Configuration["MongoDb:ConnectionString"];
var mongoDatabase = builder.Configuration["MongoDb:DatabaseName"];

// Injetar UserService com dependências
builder.Services.AddSingleton<IUserService>(sp =>
    new UserService(mongoConnection, mongoDatabase));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Redireciona raiz para Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

public partial class Program { }
