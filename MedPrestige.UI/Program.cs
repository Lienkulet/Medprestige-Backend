using MedPrestige.BLL;
using MedPrestige.DAL.Interfaces;
using MedPrestige.DAL.Repositories;
using MedPrestige.Models.Context;
using MedPrestige.Models.Mapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS pentru Next.js
builder.Services.AddCors(options =>
{
    options.AddPolicy("NextJs", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Conexiunea MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// AutoMapper
builder.Services.AddAutoMapper(cfg => MapperConfig.RegisterMappings(cfg));

// Repositories (DAL)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorDetailRepository, DoctorDetailRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IDoctorServiceRepository, DoctorServiceRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

// BusinessLogic (BLL)
builder.Services.AddScoped<BusinessLogic>();

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("NextJs");
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
