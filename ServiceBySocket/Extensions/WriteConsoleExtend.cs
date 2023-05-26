namespace ServiceBySocket.Extensions
{
    public class WriteConsoleExtend
    {
        private readonly ConsoleColor _defaultTextConsole = ConsoleColor.White;

        private readonly ConsoleColor _error = ConsoleColor.Red;
        private readonly ConsoleColor _success = ConsoleColor.Green;
        private readonly ConsoleColor _warning = ConsoleColor.Yellow;
        private readonly ConsoleColor _message = ConsoleColor.DarkGreen;

        public WriteConsoleExtend()
        {
        }

        public void Error(string message)
        {
            WriteMessage(message, _error);
        }

        public void Success(string message)
        {
            WriteMessage(message, _success);
        }

        public void Warning(string message)
        {
            WriteMessage(message, _warning);
        }

        public void WriteMessage(string message)
        {
            WriteMessage(message, _message);
        }

        private void WriteMessage(string message, ConsoleColor color)
        {
            Console.WriteLine();
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
