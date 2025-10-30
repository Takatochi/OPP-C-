using System;
using ConsoleBank.Application.Cases;
using ConsoleBank.Domain.Accounts;
using ConsoleBank.Domain.Services;
using ConsoleBank.Infrastructure.Notifications;

namespace ConsoleBank;

internal class Program
{
    static void Main()
    {
        // UTF-8 для коректного відображення української у консолі
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Підписуємо консоль на повідомлення з банку (події)
        ConsoleSubscriber.WireUp();
        
       
        
        // Створюємо фасад банку (координує роботу рахунків і сповіщення)
        var bank = new BankService();

        // Варіант А допустимо, але не бажано у шарованій архітектурі...
        // Program напряму викликає бізнес-метод сервісу.
        // var chk = bank.OpenAccount(id => new CheckingAccount(id, "Основний", "Іван", 5000m, 1000m));

        // Варіант B рекомендую через сценарій Application.
        // Program лише ініціює дію, а логіка сценарію і сервісу лишається поза інтрейфесом користувача.
        var chk = OpenAccount.Handle(
            bank,
            id => new CheckingAccount(
                id,
                "Основний",
                "Іван",
                5000m,
                1000m));

        // Другий рахунок, так само через сценарій
        var sav = OpenAccount.Handle(
            bank,
            id => new SavingsAccount(
                id,
                "Накопичення",
                "Марія",
                12000m,
                0.06m)); // 6% річних; суфікс m = decimal (точний тип для фінансів)

        // Зміни балансу теж можна напряму методами рахунку це ок,
        //але головне не встановлювати Balance вручну, щоб не обходити перевірки.
        chk.Deposit(1500m);
        chk.Withdraw(5600m);

        // Нарахування відсотків спеціальний метод класу, а не ручна формула
        //  sav.AccrueMonthlyInterest(); Не скомпілюється (: Бо базовий клас Account такого методу не має, 
        // тому робимо явне приведення типу casting 
        ((SavingsAccount)sav).AccrueMonthlyInterest();

        // Переказ.. можна викликати метод сервісу напряму,
        // але для консистентності демонструємо через сценарій Application
        TransferMoney.Handle(
            bank,
            chk.Id,
            sav.Id,
            500m);

        // можна вручну пройтись по bank.All(),
        // але краще мати окремий сценарій в Application для єдиного формату виводу
        PrintAccounts.Handle(bank);

        Console.WriteLine("\nРоботу завершено. Натисніть будь-яку клавішу...");
        Console.ReadKey();
    }
}