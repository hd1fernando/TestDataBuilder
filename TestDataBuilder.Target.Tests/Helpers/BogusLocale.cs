namespace TestDataBuilder.Target.Tests.Helpers;

public static class BogusLocale
{
    public static BrazilianBogusLocale PT_BR => new BrazilianBogusLocale();
    public static ItalianBogusLocale IT => new ItalianBogusLocale();
    public static EnglishBogusLocale EN => new EnglishBogusLocale();
}

public abstract class AbstractBogusLocale
{
    public abstract string LocaleValue();

    public override string ToString() => LocaleValue();
}

public sealed class BrazilianBogusLocale : AbstractBogusLocale
{
    public override string LocaleValue() => "pt_BR";
}

public sealed class ItalianBogusLocale : AbstractBogusLocale
{
    public override string LocaleValue() => "it";
}

public sealed class EnglishBogusLocale : AbstractBogusLocale
{
    public override string LocaleValue() => "en";
}
