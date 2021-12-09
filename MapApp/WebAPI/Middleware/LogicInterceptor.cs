using Autofac;
using Castle.DynamicProxy;
using NLog;
using System.Linq;

namespace WebAPI.Middleware
{
    public class LogicInterceptor : IInterceptor
    {

        private readonly Logger _logger;
        public LogicInterceptor() {
            _logger  = NLog.LogManager.GetCurrentClassLogger();
        }


        public void Intercept(IInvocation invocation)
        {
            
            var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));
            _logger.Info($"Calling: {name}\nArgs: {args}");
            var watch = System.Diagnostics.Stopwatch.StartNew();
                    invocation.Proceed(); //Intercepted method is executed here.
                    watch.Stop();
                    var executionTime = watch.ElapsedMilliseconds;

                    _logger.Info($"Done: result was {invocation.ReturnValue}\nExecution Time: {executionTime} ms.");
        }
    }
}
