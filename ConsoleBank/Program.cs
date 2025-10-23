using ConsoleBank.Domain.Accounts;
using ConsoleBank.Domain.Services;
using ConsoleBank.Infrastructure.Notifications;

namespace ConsoleBank;

internal class Program
{
    static void Main()
    {
        // Встановлюємо кодування UTF-8, щоб український текст коректно відображався у консолі.
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Підключаємо консоль як підписника на всі події банку.
        // Тобто, усе що робить банк (відкриття рахунку, операції) буде одразу видно на екрані.
        ConsoleSubscriber.WireUp();

        // Створюємо екземпляр банку.
        // BankService містить усі методи для роботи з рахунками.
        var bank = new BankService();

        // Створюємо поточний рахунок CheckingAccount
        // Використовуємо лямбда-функцію: вона приймає id і створює об'єкт рахунку.
        // Суфікс m означає, що число має тип decimal, цей тип використовують для фінансів.
        // Наприклад, 5000m — це 5000 гривень без похибок округлення.
        var chk = bank.OpenAccount(id => new CheckingAccount(id, "Основний", "Іван", 5000m, 1000m));

        // Створюємо заощаджувальний рахунок (SavingsAccount)
        // Тут останній параметр — це річна процентна ставка (6%).
        var sav = bank.OpenAccount(id => new SavingsAccount(id, "Накопичення", "Марія", 12000m, 0.06m));

        // Поповнюємо рахунок Івана на 1500 грн.
        chk.Deposit(1500m);

        // Знімаємо 5600 грн з поточного рахунку.
        // Якщо баланс менший, ніж потрібно, метод перевірить це і не дасть зняти зайве.
        chk.Withdraw(5600m);

        // Нараховуємо відсотки на заощаджувальному рахунку.
        // Метод сам розраховує місячний прибуток за ставкою.
        ((SavingsAccount)sav).AccrueMonthlyInterest();

        // Робимо переказ 500 грн з рахунку Івана на рахунок Марії.
        // Якщо ID рахунків однакові — метод викине помилку.
        bank.Transfer(chk.Id, sav.Id, 500m);

        // Виводимо інформацію про всі рахунки.
        Console.WriteLine("\n=== Рахунки банку ===");
        foreach (var a in bank.All())
        {
            Console.WriteLine($"[{a.GetType().Name}] ID={a.Id}, '{a.Name}', {a.Owner}, {a.Balance:C}");
        }

        // Після завершення можна натиснути будь-яку клавішу, щоб закрити консоль.
        Console.WriteLine("\nРоботу завершено. Натисніть будь-яку клавішу...");
        Console.ReadKey();
    }
}