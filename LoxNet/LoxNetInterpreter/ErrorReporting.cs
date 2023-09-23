namespace LoxNetInterpreter
{
    internal class ErrorReporting
    {

        public static Boolean hadError = false;

        public static void Error(int line, String message)
        {
            Report(line, String.Empty, message);
        }

        public static void Report(int line, String where, String message)
        {
            Console.WriteLine($"[line {line}] Error {where}: {message}");
            hadError = true;
        }
    }
}
