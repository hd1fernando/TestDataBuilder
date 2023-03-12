namespace TestDataBuilder.Target.Entities;

public sealed class EmployeeEntity : Entity<Guid>
{
    required public string Name { get; init; }
    required public string FiscalCode { get; init; }
}