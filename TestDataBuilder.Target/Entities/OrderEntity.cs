namespace TestDataBuilder.Target.Entities;

public sealed class OrderEntity : Entity<Guid>
{
    required public string Name { get; init; }
    required public int Quantity { get; init; }
    required public DateTime InTime { get; init; }
    required public CustomerEntity Customer { get; init; }
    required public decimal Value { get; init; }
}
