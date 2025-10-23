namespace ConsoleBank.Infrastructure.Notifications;

public class FileLoggerSubscriber
{
    public static void WireUp() => NotificationHub.OnNotify += LogToFile;

    private static void LogToFile(string message)
    {
        // Як би не евент це прийшлося додавати, а бац, а нас уже це є як єдиний контракт для всіх функцій
        // var logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";
        File.AppendAllText("bank_log.txt", message + Environment.NewLine);
    }
}