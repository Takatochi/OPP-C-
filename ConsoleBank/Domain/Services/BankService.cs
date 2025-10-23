using ConsoleBank.Domain.Abstractions;
using ConsoleBank.Infrastructure.Notifications;

namespace ConsoleBank.Domain.Services;

public class BankService
{
    private readonly List<Account> _accounts = new();
    private int _nextId = 1;

    public T OpenAccount<T>(Func<int, T> factory) where T : Account
    {
        var id = _nextId++;
        var acc = factory(id);
        _accounts.Add(acc);
        NotificationHub.Notify($"Відкрито рахунок {acc.Name} для {acc.Owner}");
        return acc;
    }

    public Account? FindById(int id) => _accounts.FirstOrDefault(a => a.Id == id);

    public void Transfer(int fromId, int toId, decimal amount)
    {
        var from = FindById(fromId) ?? throw new InvalidOperationException("Джерело не знайдено");
        var to   = FindById(toId)   ?? throw new InvalidOperationException("Отримувач не знайдено");
        if (fromId == toId) throw new InvalidOperationException("Неможливо переказати на той самий рахунок");
        from.Withdraw(amount);
        to.Deposit(amount);
        NotificationHub.Notify($"Переказ {amount:C} з ID={fromId} на ID={toId}");
    }

    public IReadOnlyList<Account> All() => _accounts;
}