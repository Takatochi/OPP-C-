using System;
using ConsoleBank.Domain.Abstractions;
using ConsoleBank.Domain.Services;

namespace ConsoleBank.Application.Cases;

public static class OpenAccount
{
    public static T Handle<T>(BankService bank, Func<int, T> factory) where T : Account
        => bank.OpenAccount(factory);
}