using Business_BLL.BrancheSrv;
using Business_BLL.ClientSrv;
using Business_BLL.DepartmentSrv;
using Business_BLL.EmployeeSrv;
using Business_BLL.RoleSrv;
using Business_BLL.ServiceSrv;
using Business_BLL.VacancieSrv;
using Data_DAL.Entities;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database context
builder.Services.AddDbContext<ConnectDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connect")));


builder.Services.AddScoped<IDepartment, DepartmentService>();

builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<IEmployee, EmployeeService>();
builder.Services.AddScoped<IService, ServiceService>();
builder.Services.AddScoped<IClient, ClientService>();
builder.Services.AddScoped<IVacancie, VacancieService>();
builder.Services.AddScoped<IBranche, BrancheService>();





//c?u hình xác th?c ng??i dùng b?ng Cookie
builder.Services.AddAuthentication("CookieAuthenication").AddCookie("CookieAuthenication", options =>
{
    //ch? ra ???ng d?n c?m truy c?p m?c dù ?ã ??ng nh?p
    //option.AccessDeniedPath = new PathString( "/Manager/Account");
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "c2208i",
        Path = "/"
    };

    //ch? ??nh ???ng d?n
    options.LoginPath = new PathString("/Admin/Login/Index");
    options.ReturnUrlParameter = "UrlRedirect";
    options.SlidingExpiration = true;


});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ConnectDB>();

    SeedAdminUser(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // For development environment
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS for production
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static files

app.UseRouting();

app.UseAuthentication(); // Ensure this is before UseAuthorization
app.UseAuthorization();

// Cấu hình định tuyến cho Areas
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
void SeedAdminUser(ConnectDB context)
{
    // Ensure Admin Department exists
    var department = context.Departments.FirstOrDefault(d => d.Name == "Admin");
    if (department == null)
    {
        department = new Department
        {
            //Id = 1,
            Name = "Admin"
        };
        context.Departments.Add(department);
        context.SaveChanges();
    }

    // Ensure Admin Role exists
    var role = context.Roles.FirstOrDefault(r => r.Name == "Admin");
    if (role == null)
    {
        role = new Role
        {
            //Id =1,
            Name = "Admin"
        };
        context.Roles.Add(role);
        context.SaveChanges();
    }

    // Check if admin already exists
    if (!context.Employees.Any(u => u.Username == "Admin"))
    {
        var admin = new Employee
        {
            Username = "Admin",
            Email = "Admin@gmail.com",
            RoleId = 1, //Admin role
            DepartmentId = 1, //department
            Password = Utilitie.GetMD5HashData("12345"),
            Name = "KimVanDang",
            Avata = "/images/admin.jpg",
            Address = "Ha Noi",
            Phone = "0987264721",
        };

        context.Employees.Add(admin);
        context.SaveChanges();
    }
}



