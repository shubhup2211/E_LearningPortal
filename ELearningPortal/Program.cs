using System.Text;
using ELearning.Data;
using ELearningPortal.Helpers;
using ELearningPortal.Interfaces;
using ELearningPortal.Interfaces.IAdmin;
using ELearningPortal.Interfaces.ISuperAdmin;
using ELearningPortal.Interfaces.IUser;
using ELearningPortal.Services;
using ELearningPortal.Services.Admin;
using ELearningPortal.Services.SuperAdmin;
using ELearningPortal.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

// -------- Authentication (JWT via HttpOnly cookie) --------
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        // Read the JWT from the HttpOnly cookie ("AuthToken") on every request.
        // Also redirect to /Account/Login when unauthorized so MVC feels natural.
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue("AuthToken", out var token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.Redirect("/Account/Login");
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                context.Response.Redirect("/Account/AccessDenied");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// -------- Helpers & Services --------
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<EmailHelper>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<ICourseApprovalService, CourseApprovalService>();
builder.Services.AddScoped<ISubscriptionApprovalService, SubscriptionApprovalService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IMyCourseService, MyCourseService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT: Authentication MUST come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
