using System;

namespace ConsoleBank.Infrastructure.Notifications;

public static class ConsoleSubscriber
{
    // Підписуємо консоль на NotificationHub.
    public static void WireUp() => NotificationHub.OnNotify += Console.WriteLine;
    // ну можна і так... 
    // public static void WireUp()
    // {
    //     NotificationHub.OnNotify += Console.WriteLine;
    // }
}