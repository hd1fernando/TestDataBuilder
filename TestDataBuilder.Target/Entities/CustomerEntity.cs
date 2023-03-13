namespace TestDataBuilder.Target.Entities;

public class CustomerEntity : Entity<Guid>
{
    required public string Name { get; init; }
}
