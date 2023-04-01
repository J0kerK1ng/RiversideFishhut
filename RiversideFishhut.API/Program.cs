using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RiversideFishhut.API.Data;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("RiversideFishhutDbConnectionString");
builder.Services.AddDbContext<RiversideFishhutDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		b => b.AllowAnyHeader()
			.AllowAnyOrigin()
			.AllowAnyMethod());
});

var jwtSettings = builder.Configuration.GetSection("JWT");
var jwtIssuer = jwtSettings["Issuer"];
var jwtAudience = jwtSettings["Audience"];
var jwtKey = jwtSettings["Key"];

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

		options.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = context =>
			{
				if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
				{
					Log.Information("Token expired");

					context.Response.Headers.Add("Token-Expired", "true");
					var customResponse = new CustomTokenResponse(401, "Token expired", null);
					context.Response.StatusCode = customResponse.Status;
					context.Response.ContentType = "application/json";
					context.Response.WriteAsync(JsonConvert.SerializeObject(customResponse));
				}
				else if (context.Exception.GetType() == typeof(SecurityTokenInvalidLifetimeException))
				{
					Log.Information("Invalid token");

					var customResponse = new CustomTokenResponse(401, "Invalid token", null);
					context.Response.StatusCode = customResponse.Status;
					context.Response.ContentType = "application/json";
					context.Response.WriteAsync(JsonConvert.SerializeObject(customResponse));
				}
				else if (context.Exception.GetType() == typeof(SecurityTokenException))
				{
					Log.Information("Invalid token format");

					var customResponse = new CustomTokenResponse(401, "Invalid token format", null);
					context.Response.StatusCode = customResponse.Status;
					context.Response.ContentType = "application/json";
					context.Response.WriteAsync(JsonConvert.SerializeObject(customResponse));
				}

				context.Fail(context.Exception);
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddAuthorization();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "RiversideFishhut.API", Version = "v1" });
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RiversideFishhut.API v1"));
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Use(async (context, next) =>
{
	try
	{
		await next.Invoke();
	}
	catch (TokenException ex)
	{
		context.Response.StatusCode = ex.CustomTokenResponse.Status;
		context.Response.ContentType = "application/json";
		await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.CustomTokenResponse));
	}
});

app.Run();









