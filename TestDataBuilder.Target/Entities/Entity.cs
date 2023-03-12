namespace TestDataBuilder.Target.Entities;

public abstract class Entity<TType>
{
    public TType Code { get; protected set; }
}