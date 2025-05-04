using Microsoft.EntityFrameworkCore;
using realationshipss.data;
using realationshipss.services;
using realationshipss.Middleware;
using realationshipss.security;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Datacontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });

});
//    <using custom jwtmiddlware istead of 1)service.AddAuthentication and 2)service.AddAuhorization>
////configuring jwt authentication to check
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(o =>
//    { 
//        o.RequireHttpsMetadata = true;  // we put it only false for testing that diable https if it false
//        o.TokenValidationParameters = new TokenValidationParameters
//        {
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:SecretKey"]!)),
//            //ValidIssuer = "Bookapp", //change thsi to take from appsetting .json
//            ValidIssuer = builder.Configuration["jwt:Issuer"],
//            ValidAudience = builder.Configuration["jwt:Audience"],
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ClockSkew = TimeSpan.Zero
//        };
//        // do we need to use the logging middlware
//        o.Events = new JwtBearerEvents
//        {
//            OnAuthenticationFailed = context =>
//            {
//                Console.WriteLine($"token validation faild: { context.Exception.Message} ");
//                return Task.CompletedTask;
//            }
//        };
//    });

//builder.Services.AddAuthorization();


// is there is a problem here in when here it scans all the assemblies
// (all classes that inherted from the Profile class)
// is there will be a problem in the future as it will make promlems ??  (askit)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();
builder.Services.AddScoped<JwtProvider>();
builder.Services.AddScoped<EmailService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseMiddleware<JwtMiddlware>();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>(); //if ther is any middle after it and hteris no expetion so make it first 
//app.UseCors(x => x
//             .SetIsOriginAllowed(origin => true)
//             .AllowAnyMethod()
//             .AllowAnyHeader()
//             .AllowCredentials());

app.UseAuthentication();  // will remove this and make jwt middleware
app.UseAuthorization();   // instad of both authentication and authroization

app.MapControllers();

app.Run();
