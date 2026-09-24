using learnllm.Config;
using learnllm.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourceBuilder.UseVector();  // This allows Npgsql/pgvector integration to understand PostgreSQL vector values.
var dataSource = dataSourceBuilder.Build();

// Add services to the container.
#pragma warning disable OPENAI001
builder.Services.AddScoped<ILlmService, LlmService>();
#pragma warning restore OPENAI001
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IEmbeddingService, EmbeddingService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IRagService, RagService>();
builder.Services.AddSingleton(dataSource);

builder.Services.Configure<LlmOptions>(
    builder.Configuration.GetSection("LLM"));
builder.Services.Configure<OpenAIOptions>(
    builder.Configuration.GetSection("OpenAI"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();