using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using testeLeadSoft.Data;
using testeLeadSoft.Services.ArticleService;
using testeLeadSoft.Services.AuthorService;
using testeLeadSoft.Services.CategoryService;
using testeLeadSoft.Services.CommentService;

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

// Para adicionar o autoMapper (depois de instalar a dependencia)
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();

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
