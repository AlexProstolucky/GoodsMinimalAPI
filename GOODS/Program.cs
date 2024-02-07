using Goods;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        Goods_TODO handler = new(new List<Good>());
        app.Use((context, next) =>
        {
            context.Items["Handler"] = handler;
            var responce = context.Response;
            var request = context.Request;
            return next(context);
        });
        app.UseMiddleware<Routing>();
        app.UseStaticFiles();
        app.Run();
    }
}