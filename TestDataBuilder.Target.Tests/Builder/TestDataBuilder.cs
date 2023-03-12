namespace TestDataBuilder.Target.Tests.Builder;

public abstract class TestDataBuilder<TEntity, TBuilder> where TEntity : class
{
    
    protected bool ButHasBeenFlaged = false;

    protected Faker Faker { get; private init; }
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
