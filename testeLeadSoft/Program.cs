using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using testeLeadSoft.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
	s.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "testeLeadSoft API",
		Description = "A sample ASP.NET API",
		Contact = new OpenApiContact
		{
			Name = "Lucas Machado",
			Email = "lmachado72@outlook.com",
			Url = new Uri("https://github.com/luvvas")
		},
		Version = "v1"
	});

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

	s.IncludeXmlComments(xmlPath);
});

builder.Services.AddEntityFrameworkNpgsql()
	.AddDbContext<DataContext>(options =>
	{
		options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
	});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
