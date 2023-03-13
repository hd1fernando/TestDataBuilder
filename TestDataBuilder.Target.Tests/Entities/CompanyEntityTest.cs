using TestDataBuilder.Target.Tests.Builder;


namespace TestDataBuilder.Target.Tests.Entities;

public class OrderEntityTest
{
    private OrderEntityBuilder AnOrder()
        => new OrderEntityBuilder(BogusLocale.EN);

    private CustomerEntityBuilder AnCustomer()
        => new(BogusLocale.EN);


    [Fact]
    public void Test1()
    {
        var theCustomer = AnCustomer().Build();

        var order = AnOrder()
            .From(theCustomer)
            .Build();   
    }

}


public class CompanyEntityTest
{

    private CompanyEntityBuilder AnBrazilianCompanyWithNameAndFancyName()
        => new CompanyEntityBuilder(BogusLocale.PT_BR)
            .WithRandomCorportateName()
            .WithRandomFancyName();

    private CompanyEntityBuilder AnBrazilianCompany()
        => new CompanyEntityBuilder(BogusLocale.PT_BR)
            .WithAllRandom();


    private EmployeeEntityBuilder AnItalianEmployee()
        => new(BogusLocale.IT);

    [Fact]
    public void Test()
    {

        var company = AnBrazilianCompanyWithNameAndFancyName().Build();
    }

    [Fact]
    public void Test2()
    {
        var company = AnBrazilianCompanyWithNameAndFancyName()
            .But()
            .WithRandomBrazilianFiscalCodeNumber()
            .WithEmployees(_ => _
                .WithRandomName()
                .WithRandomBrazilianFiscalCode()
                )
            .And().WithRandonLastUpdate()
            .Build();
    }

    [Fact]
    public void Test3()
    {
        var company = AnBrazilianCompany()
            .But()
            .WithRandomCorportateName()
            .WithFancyName("Microsoft LTDA")
            .WithRandonLastUpdate()
            .And()
            .WithRandomBrazilianFiscalCodeNumber()
            .WithUniqueFiscalCodes()
            .WithEmployees
            (_ => _
                    .WithRandomName()
                    .WithRandomItalianFiscalCode()
                    .Build()
            ).Build(5);

    }

    [Fact]
    public void Test4()
    {
        var company = AnBrazilianCompany()
            .But()
            .With(AnItalianEmployee()
                .WithRandomName()
                .WithRandomItalianFiscalCode()
                )
            .And().With(AnItalianEmployee()
                .WithName("Pietro"))
            .Build();

    }

    [Fact]
    public void Test5()
    {
        var company = AnBrazilianCompany().Build();
    }
}
