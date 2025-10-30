using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleBank.Domain.Abstractions;
using ConsoleBank.Infrastructure.Notifications;

namespace ConsoleBank.Domain.Services;

public class BankService
{
    // Список усіх рахунків банку для внутрішньої колекції
    private readonly List<Account> _accounts = new();

    // Лічильник для автоматичного призначення ID новим рахункам
    private int _nextId = 1;

    // Метод створення нового рахунку
    // Приймає функцію (factory), яка створює конкретний тип рахунку
    public T OpenAccount<T>(Func<int, T> factory) where T : Account
    {
        var id = _nextId++; // генеруємо унікальний ID
        var acc = factory(id); // створюємо рахунок через передану функцію
        _accounts.Add(acc); // додаємо до списку банку

        // Повідомляємо через NotificationHub (event)
        NotificationHub.Notify($"Відкрито рахунок {acc.Name} для {acc.Owner}");
        return acc;
    }

    // Метод для пошуку рахунку за ID
    // Якщо не знайдено — повертає null
    public Account? FindById(int id) => _accounts.FirstOrDefault(a => a.Id == id);

    // Метод переказу коштів між двома рахунками
    // Виконує перевірки, щоб уникнути некоректних дій
    public void Transfer(int fromId, int toId, decimal amount)
    {
        var from = FindById(fromId) ?? throw new InvalidOperationException("Джерело не знайдено");
        var to   = FindById(toId)   ?? throw new InvalidOperationException("Отримувач не знайдено");

        if (fromId == toId)
            throw new InvalidOperationException("Неможливо переказати на той самий рахунок");

        from.Withdraw(amount); // списуємо з одного
        to.Deposit(amount);    // зараховуємо на інший

        // Відправляємо сповіщення
        NotificationHub.Notify($"Переказ {amount:C} з ID={fromId} на ID={toId}");
    }

    // Метод повертає список усіх рахунків (тільки для читання)
    public IReadOnlyList<Account> All() => _accounts;
}