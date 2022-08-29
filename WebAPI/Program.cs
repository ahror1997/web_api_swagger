using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DB Connection
string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    // mssql
    //options.UseSqlServer(mySqlConnectionStr);

    // mysql
    //options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));

    // postgresql
    options.UseNpgsql(mySqlConnectionStr);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cars API",
        Description = "Cars API for learning ASP.NET CORE Web API",
        Contact = new OpenApiContact
        {
            Name = "Akhror Ziyodinov",
            Email = "ahror.ziyodinov@gmail.com",
            Url = new Uri("https://twitter.com/AhrorZiyodinov"),
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT"),
        },
        Version = "v1"
    });

    // generate the xml docs that'll drive the swagger docs
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
