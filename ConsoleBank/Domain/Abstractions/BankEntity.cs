namespace ConsoleBank.Domain.Abstractions;

public abstract class BankEntity
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    protected BankEntity(int id, string name) { Id = id; Name = name; }
    public abstract void PrintSummary();
}