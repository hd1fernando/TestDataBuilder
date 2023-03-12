using Bogus.DataSets;
using TestDataBuilder.Target.Entities;
using TestDataBuilder.Target.Tests.Builder;
using Xunit;

namespace TestDataBuilder.Target.Tests.Entities;

public class BogusLocale
{
    public const string PT_BR = "pt_BR";
    public const string IT = "it";
}

public class CompanyEntityTest
{
    private CompanyEntityBuilder _companyBuilder;

    public CompanyEntityTest()
    {
        _companyBuilder = new CompanyEntityBuilder(BogusLocale.PT_BR)
            .WithRandomCorportateName()
            .WithRandomFancyName();
    }

    [Fact]
    public void Test()
    {

        var company = _companyBuilder.Build();
    }

    [Fact]
    public void Test2()
    {
        var company = _companyBuilder
            .But()
            .WithRandomBrazilianFiscalCodeNumber()
            .WithEmployees(_ => _
                .WithRandomName()
                .WithRandomBrazilianFiscalCode()
                )
            .Build();
    }

    [Fact]
    public void Test3()
    {
        var company = new CompanyEntityBuilder(BogusLocale.IT)
            .WithRandomCorportateName()
            .WithFancyName("Microsoft LTDA")
            .WithEmployee(
                new EmployeeEntityBuilder(BogusLocale.IT)
                    .WithRandomName()
                    .WithRandomItalianFiscalCode()
                    .Build()
            ).Build(5);

    }
}
