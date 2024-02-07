namespace Goods
{
    public class Routing
    {
        private readonly RequestDelegate _next;
        public bool IsLogin = false;
        public Routing(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var responce = context.Response;
            var request = context.Request;
            Goods_TODO handler = (Goods_TODO)context.Items["Handler"];


            if (request.Path == "/" && request.Method == "POST")
            {

                if (request.Form["username"] == "admin" && request.Form["password"] == "admin")
                {
                    IsLogin = true;
                    Console.WriteLine("TYDAA YOGO");
                }
            }
            if (request.Path == "/")
            {
                responce.ContentType = "text/html; charset= utf-8";
                await context.Response.SendFileAsync("main.html");
            }


            if ((request.Path == "/good") && request.Method == "POST")
            {
                string id = request.Form["id"];
                string name = request.Form["name"];
                string des = request.Form["description"];
                var form = await context.Request.ReadFormAsync();
                var file = form.Files.GetFile("image");
                if (!handler.Is(id))
                {
                    if (file != null && file.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", $"{id}.png");
                        handler.Add(new Good(id, name, des, filePath));
                        using var stream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    if (file != null && file.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", $"{id}.png");
                        handler.Update(id, name, des, filePath);
                        using var stream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(stream);
                    }
                }

            }
            if (context.Request.Path == "/good")
            {
                responce.ContentType = "text/html; charset= utf-8";
                await context.Response.SendFileAsync("create_add.html");
            }
            if (request.Path == "/goods" && IsLogin)
            {
                var goods = handler.GetGoods();
                var htmlContent = "<!DOCTYPE html><html><head><title>Goods</title></head><body><h1>List of Goods</h1><ul>";

                foreach (var item in goods)
                {
                    htmlContent += $"<li><strong>ID:</strong> {item.id}<br/><strong>Name:</strong> {item.name}<br/><strong>Description:</strong> {item.description}<br/>";
                    htmlContent += $"<img src='/uploads/{item.id}.png' alt='{item.name}' width='100'/></li><br/>";
                }

                htmlContent += "</ul></body></html>";

                await context.Response.WriteAsync(htmlContent);
            }
            await _next(context);
        }
    }
}
