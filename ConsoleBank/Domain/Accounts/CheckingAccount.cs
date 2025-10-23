using System;
using ConsoleBank.Domain.Abstractions;

namespace ConsoleBank.Domain.Accounts;

public class CheckingAccount : Account
{
    public decimal OverdraftLimit { get; }
    public CheckingAccount(int id, string name, string owner, decimal startBalance, decimal overdraftLimit)
        : base(id, name, owner, startBalance) => OverdraftLimit = overdraftLimit;

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (Balance + OverdraftLimit < amount) throw new InvalidOperationException("Перевищено овердрафтний ліміт");
        Balance -= amount;
    }
}