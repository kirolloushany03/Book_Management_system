using Azure.Core;
using Microsoft.VisualBasic;

namespace realationshipss.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Incoming request Method: {context.Request.Method}\n\nPath:{context.Request.Path}");

            //for depugging and to try it
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put || context.Request.Method == HttpMethods.Delete)
            {
                // EnableBuffering allows the request body to be read multiple times instead of just once.
                // By default, the request body is a forward-only stream and can be read only once.
                // Without this, if the body is read in the middleware (e.g., for logging), it will no longer be available in the controller.
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                Console.WriteLine($"Request body: {body}");
                context.Request.Body.Position = 0;
            }
            // calling the next middle ware to continue
            await _next(context);
            
        }
    }



    //for buffering 
    //⚠️ Caution
    //Performance Impact: Buffering large request bodies can use a lot of memory or disk I/O.Be mindful when enabling buffering for large files or streaming data.
    //Security Concerns: Avoid logging sensitive information(e.g., passwords or personally identifiable information).
}


//authentication contoller + mail service will dowinlding it by package (mailkit)  and will put smtp credctionals make endpoint test take the emial as qusry param and sent to the emil
//free smtp servers serach about it
// will give us project sample to steal from it 
// will take form setal form it s
// we need to login to use our conrollers
// make base entity that all the enitity will inhert form it that has  creadet at , updated at
//these fileds database interseptor made save async  // so our code will go the interceptor then database
//will make function inside the interceptor to if sth new will put vale of sth


//authentication with twab3 with the project that will gave to us
//make mail service in 4 lines take any email and sent this email anything  make endpoint named test 
//base entities on all entites has updated and creadetat 
// make the overide on the data base to automatically give us the updated at and created at so the easier is to overide savechangeasync
