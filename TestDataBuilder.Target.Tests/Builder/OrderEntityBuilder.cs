using TestDataBuilder.Target.Entities;

namespace TestDataBuilder.Target.Tests.Builder;

public class OrderEntityBuilder : TestDataBuilder<OrderEntity, OrderEntityBuilder>
{
    public OrderEntityBuilder(AbstractBogusLocale locale) : base(locale)
    {
    }

    private string _name { get; set; }
    private int _quantity { get; set; }
    private DateTime _inTime { get; set; }
    private CustomerEntity _customer { get; set; }
    private decimal _value { get; set; }


    public OrderEntityBuilder From(CustomerEntity customerEntity)
    {
        _customer = customerEntity;
        return this;
    }


    public override OrderEntityBuilder And() => this;

    public override OrderEntity Build()
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<OrderEntity> Build(int num)
    {
        throw new NotImplementedException();
    }

    public override OrderEntityBuilder But() => this;
}
