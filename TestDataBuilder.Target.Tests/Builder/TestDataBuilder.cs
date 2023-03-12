using Bogus;

namespace TestDataBuilder.Target.Tests.Builder;

public abstract class TestDataBuilder<TEntity, TBuilder> where TEntity : class
{
    public string Locale { get; private set; }
    protected Faker Faker;
    public TestDataBuilder(string locale)
    {
        if (string.IsNullOrEmpty(locale))
            throw new ArgumentException(nameof(locale), "Locale is required");

        Locale = locale;
        Faker = new Faker(locale);

    }

    public abstract TBuilder But();

    public abstract TEntity Build();
    public abstract IEnumerable<TEntity> Build(int num);
}
