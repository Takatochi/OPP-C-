using System;

namespace ConsoleBank.Infrastructure.Notifications;

public static class NotificationHub

{
    
    // Створюємо список подій усіх підписників на сповіщення.
    public static event NotificationHandler? OnNotify;
    
    // Метод для відправки повідомлення всім підписникам.
    public static void Notify(string message) => OnNotify?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
    // ну можна і так... 
    // public static void Notify(string message)
    // {
            // альтернатива ?.
    //     // if (OnNotify is null)
    //     // {
    //     //     return;
    //     // }
    //     // ?.Invoke — викликати, якщо список підписників не порожній.
    //     OnNotify?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
    // }
}