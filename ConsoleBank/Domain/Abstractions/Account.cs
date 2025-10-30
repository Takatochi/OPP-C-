using System;

namespace ConsoleBank.Domain.Abstractions;

public abstract class Account : BankEntity
{
    public decimal Balance { get; protected set; }
    public string Owner { get; protected set; }
    protected Account(int id, string name, string owner, decimal startBalance = 0m) : base(id, name)
    { Owner = owner; Balance = startBalance; }

    // Поповнення із базовою перевіркою.
    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Balance += amount;
    }
    
    // Зняття із перевіркою на достатність коштів.
    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (amount > Balance) throw new InvalidOperationException("Недостатньо коштів");
        Balance -= amount;
    }
    // Вивід про рахунок.
    public override void PrintSummary() =>
        Console.WriteLine($"[{GetType().Name}] ID={Id}, '{Name}', {Owner}, {Balance:C}");
}