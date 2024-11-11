using BlazorWeb.Components;
using BlazorWeb.Services;
using BlazorWeb.Services.HTTP;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped(sp => new HttpClient() {
    BaseAddress = new Uri("http://localhost:5298")
});
builder.Services.AddScoped<IUserService, UserHttp>();
builder.Services.AddScoped<ICommentService, CommentHttp>();
builder.Services.AddScoped<IPostService, PostHttp>();
builder.Services.AddScoped<IChannelService, ChannelHttp>();
builder.Services.AddHttpClient();
var app = builder.Build();
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();