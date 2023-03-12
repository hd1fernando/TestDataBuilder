using Bogus.Extensions.Brazil;
using Bogus.Extensions.Italy;
using TestDataBuilder.Target.Entities;

namespace TestDataBuilder.Target.Tests.Builder;

public class EmployeeEntityBuilder : TestDataBuilder<EmployeeEntity, EmployeeEntityBuilder>
{
    #region Entity properties

    private string _name { get; set; }
    private string _fiscalCode { get; set; }

    #endregion

    #region  Flags

    private CountryOfPerson _countryOfPerson = CountryOfPerson.OTHER;
    private bool _uniqueFiscalCode = false;

    #endregion

    public EmployeeEntityBuilder(AbstractBogusLocale locale) : base(locale) { }

    public EmployeeEntityBuilder WithRandomName()
    {
        _name = Faker.Person.FullName;
        return this;
    }

    public EmployeeEntityBuilder WithRandomBrazilianFiscalCode()
    {
        _fiscalCode = Faker.Person.Cpf();
        _countryOfPerson = CountryOfPerson.BRAZIL;
        return this;
    }

    public EmployeeEntityBuilder WithRandomItalianFiscalCode()
    {
        _fiscalCode = Faker.Person.CodiceFiscale();
        _countryOfPerson = CountryOfPerson.ITALY;
        return this;
    }

    public EmployeeEntityBuilder WithUniqueFiscalCode()
    {
        _uniqueFiscalCode = true;
        return this;
    }

    public EmployeeEntityBuilder WithAllRandom()
    {
        _name = Faker.Person.FullName;
        _fiscalCode = GenNumber();
        return this;

        string GenNumber()
        {
            switch (Locale)
            {
                case BrazilianBogusLocale:
                    _countryOfPerson = CountryOfPerson.BRAZIL;
                    return Faker.Person.Cpf();
                case ItalianBogusLocale:
                    _countryOfPerson = CountryOfPerson.ITALY;
                    return Faker.Person.CodiceFiscale();
                default:
                    throw new NotImplementedException("Plase, add a new type of locale here.");
            }
        }
    }

    public EmployeeEntityBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public override EmployeeEntity Build()
    {
        return new Faker<EmployeeEntity>(Locale.LocaleValue())
            .RuleFor(c => c.Code, f => Guid.NewGuid())
            .RuleFor(c => c.Name, _name)
            .RuleFor(c => c.FiscalCode, _fiscalCode)
            .Generate();
    }

    public override IEnumerable<EmployeeEntity> Build(int num)
    {
        return new Faker<EmployeeEntity>(Locale.LocaleValue())
            .RuleFor(c => c.Code, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => GenName(f))
            .RuleFor(c => c.FiscalCode, f => GenFiscalCode(f))
            .Generate(num);

        string GenName(Faker f) => string.IsNullOrEmpty(_name) ? _name : f.Company.CompanyName();


        string GenFiscalCode(Faker f)
        {
            if (string.IsNullOrEmpty(_fiscalCode) || _uniqueFiscalCode)
                return _fiscalCode;

            return _countryOfPerson switch
            {
                CountryOfPerson.BRAZIL => f.Person.Cpf(),
                CountryOfPerson.ITALY => f.Person.CodiceFiscale(),
                _ => throw new InvalidOperationException($"{nameof(_countryOfPerson)} not defined")
            };
        }

    }

    public override EmployeeEntityBuilder But()
    {
        ButHasBeenFlaged = true;
        return this;
    }

    public override EmployeeEntityBuilder And() => this;
}
