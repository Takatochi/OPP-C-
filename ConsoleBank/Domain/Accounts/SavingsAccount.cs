using System;
using ConsoleBank.Domain.Abstractions;

namespace ConsoleBank.Domain.Accounts;

public class SavingsAccount : Account
{
    public decimal AnnualRate { get; }
    public SavingsAccount(int id, string name, string owner, decimal startBalance, decimal annualRate)
        : base(id, name, owner, startBalance)
    { if (annualRate < 0) throw new ArgumentOutOfRangeException(nameof(annualRate)); AnnualRate = annualRate; }

    // Перевизначене зняття методу с Account класа той що virtual)xD ... враховуємо ліміт овердрафту.
    public void AccrueMonthlyInterest()
    {
        var m = AnnualRate / 12m;
        var add = Math.Round(Balance * m, 2);
        if (add > 0) Balance += add;
    }
}