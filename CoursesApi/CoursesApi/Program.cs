using System.Text.Json.Serialization;
using WebApiShared.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<OperationCancelledExceptionFilterAttribute>())
    .AddJsonOptions(options => {
    // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Always; // leaves prop: null out of json response
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // show enums as strings instead of ints automatically
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<CourseCatalog>();
builder.Services.AddTransient<IProvideOfferings, OfferingsCatalogApiCall>();

// Adapters
builder.Services.AddDbContext<CoursesDataContext>(config => { config.UseSqlServer(builder.Configuration.GetConnectionString("courses-db")); });

// typed http client, can add config/proxy settings, etc.
builder.Services.AddHttpClient<OfferingsApiAdapter>(config => 
{ 
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("offerings-api"));
    config.DefaultRequestHeaders.Add("User-Agent", "offerings-api");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
