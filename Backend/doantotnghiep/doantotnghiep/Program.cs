using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Service;
using Core.Services;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
	o.SaveToken = true;
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidAudience = builder.Configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Key),
		ClockSkew = TimeSpan.Zero
	};
	o.Events = new JwtBearerEvents
	{
		OnAuthenticationFailed = context => {
			if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
			{
				context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
			}
			return Task.CompletedTask;
		}
	};
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoAnTotNghiep", Version = "v1" });
});

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));//Do base nó dùng kiểu Generic nên phải dùng typeof để nó hiểu là kiểu gì
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IEvaluationService, EvaluationService>();
builder.Services.AddScoped<IEvaluationRepository, EvaluationRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IReceiptDetailService, ReceiptDetailService>();
builder.Services.AddScoped<IReceiptDetailRepository, ReceiptDetailRepository>();
builder.Services.AddScoped<ISuplierService, SuplierService>();
builder.Services.AddScoped<ISuplierRepository, SuplierRepository>();
builder.Services.AddScoped<IOverviewService, OverviewService>();
builder.Services.AddScoped<IOverviewRepository, OverviewRepository>();
builder.Services.AddScoped<INotifyService, NotifyService>();
builder.Services.AddScoped<INotifyRepository, NotifyRepository>();
builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddCors(c =>

    c.AddPolicy("AllowOrigin", options => {
        options
//.AllowAnyOrigin()
.WithOrigins("http://localhost:8080")
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials();
    })
    
);
builder.Services.AddSignalR().AddJsonProtocol(options => {
	options.PayloadSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoAnTotNghiep v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowOrigin");
app.UseAuthentication(); // This need to be added	
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
	endpoints.MapHub<ProductHub>("/commentHub");
});

app.Run();
