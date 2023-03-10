using System.Diagnostics;

namespace Pixeler.Source.Services;

public static class Logger
{
    public static void Log<T>(this T _, string message) => Debug.WriteLine("\t" + message, typeof(T).Name);
    public static void Log<T>(this T classToLog, object o) => classToLog.Log(o.ToString());
    public static void TimedDelegate<T>(this T classToLog, Action action, string methodName)
    {
        var start = DateTime.Now;

        action();

        var end = DateTime.Now;

        classToLog.Log($"{methodName} execution took {end - start}");
    }
}
