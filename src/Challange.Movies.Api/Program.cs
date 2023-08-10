using AutoMapper;
using Challange.Movies.Api.DBSeed;
using Challange.Movies.Api.Dependencies;
using Challange.Movies.Api.GlobalExceptionHandler;
using Challange.Movies.Api.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services
//        .AddSingleton(provider
//            => new MapperConfiguration(configure
//                => configure.AddProfile(new MappingProfile())).CreateMapper());


var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.InjectRepositories();
builder.Services.InjectServices();
builder.Services.InjectDbContext();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthorization();

SeedCinemaData.Initialize(app);

app.MapControllers();
app.MapGet("/Health", () => { return "Api Working Fine"; });
app.UseExceptionHandlerMiddleware();

app.Run();
