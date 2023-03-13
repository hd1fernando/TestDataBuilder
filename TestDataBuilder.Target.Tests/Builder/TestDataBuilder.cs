using TestDataBuilder.Target.Entities;

namespace TestDataBuilder.Target.Tests.Builder;

public class CustomerEntityBuilder : TestDataBuilder<CustomerEntity, CustomerEntityBuilder>
{
    public CustomerEntityBuilder(AbstractBogusLocale locale) : base(locale)
    {
    }

    public override CustomerEntityBuilder And()
    {
        throw new NotImplementedException();
    }

    public override CustomerEntity Build()
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<CustomerEntity> Build(int num)
    {
        throw new NotImplementedException();
    }

    public override CustomerEntityBuilder But()
    {
        throw new NotImplementedException();
    }
}

public abstract class TestDataBuilder<TEntity, TBuilder> where TEntity : class
{

    protected bool ButHasBeenFlaged = false;

    protected Faker Faker { get; private set; }
    public AbstractBogusLocale Locale { get; private set; }

    public TestDataBuilder(AbstractBogusLocale locale)
    {
        ArgumentNullException.ThrowIfNull(locale, nameof(locale));

        Locale = locale;
        Faker = new Faker(Locale.ToString());

    }



    public abstract TBuilder But();
    public abstract TBuilder And();

    public abstract TEntity Build();
    public abstract IEnumerable<TEntity> Build(int num);
}
