using System;
using ConsoleBank.Domain.Services;

namespace ConsoleBank.Application.Cases;

public static class PrintAccounts
{
    public static void Handle(BankService bank)
    {
        Console.WriteLine("\n Рахунки банку ");
        foreach (var a in bank.All())
            Console.WriteLine($"[{a.GetType().Name}] ID={a.Id}, '{a.Name}', {a.Owner}, {a.Balance:C}");
    }
}