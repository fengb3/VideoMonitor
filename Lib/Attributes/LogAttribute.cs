using PostSharp.Aspects;

namespace Lib.Attributes;


[Serializable]
public class LogAttribute : OnMethodBoundaryAspect
{
    
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Entering {args.Method.DeclaringType?.FullName}.{args.Method.Name}");
    }
    
    public override void OnExit(MethodExecutionArgs args)
    {
        Console.WriteLine($"Exiting {args.Method.Name}");
    }
}