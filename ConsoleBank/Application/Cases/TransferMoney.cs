using ConsoleBank.Domain.Services;

namespace ConsoleBank.Application.Cases;

public static class TransferMoney
{
    public static void Handle(BankService bank, int fromId, int toId, decimal amount)
        => bank.Transfer(fromId, toId, amount);
}