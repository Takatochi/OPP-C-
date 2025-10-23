using ConsoleBank.Domain.Services;

namespace ConsoleBank.Infrastructure.Notifications;

public static class NotificationHub
{
    public static event NotificationHandler? OnNotify;
    public static void Notify(string message) => OnNotify?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
}