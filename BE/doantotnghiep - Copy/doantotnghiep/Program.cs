using Core.Interfaces;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Service;
using Core.Services;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString= builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoAnTotNghiep", Version = "v1" });
});
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));//Do base nó dùng kiểu Generic nên phải dùng typeof để nó hiểu là kiểu gì
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ICombinationSubjectService, CombinationSubjectService>();
builder.Services.AddScoped<ICombinationSubjectRepository, CombinationSubjectRepository>();
builder.Services.AddScoped<ITeacherDepartmentRepository, TeacherDepartmentRepository>();
builder.Services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
builder.Services.AddCors(c =>

    c.AddPolicy("AllowOrigin", options => options
    .WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader()
    )
);
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoAnTotNghiep v1"));
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowOrigin");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
