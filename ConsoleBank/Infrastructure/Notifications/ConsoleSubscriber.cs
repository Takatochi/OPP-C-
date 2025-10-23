namespace ConsoleBank.Infrastructure.Notifications;

public static class ConsoleSubscriber
{
    public static void WireUp() => NotificationHub.OnNotify += Console.WriteLine;
}