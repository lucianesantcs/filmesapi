var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adicionar esse trecho em SQL SERVER
builder.Services.AddDbContext<FilmeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FilmeConnection"));
});

//em MYSQL
builder.Services.AddDbContext<FilmeContext>(options =>
{
    options.UseMySQLServer(builder.Configuration.GetConnectionString("FilmeConnection"));
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
