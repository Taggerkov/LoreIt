using LocalImpl;
using RepoContracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepo, UserLocal>();
builder.Services.AddScoped<IPostRepo, PostLocal>();
builder.Services.AddScoped<ICommentRepo, CommentLocal>();
builder.Services.AddScoped<IChannelRepo, ChannelLocal>();
var app = builder.Build();
app.MapControllers();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.Run();