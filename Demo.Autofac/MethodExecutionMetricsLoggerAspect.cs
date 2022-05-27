using Castle.DynamicProxy;
using System.Diagnostics;

namespace Demo.Autofac
{
    public class MethodExecutionMetricsLoggerAspect : IInterceptor
    {
        public MethodExecutionMetricsLoggerAspect()
        {

        }
   
        public void Intercept(IInvocation invocation)
        {
            Debug.WriteLine("Inside interceptor");
            invocation.Proceed();
        }
    }
}