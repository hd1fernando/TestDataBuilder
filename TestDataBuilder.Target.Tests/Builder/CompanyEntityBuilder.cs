using Bogus.Extensions.Brazil;
using Bogus.Extensions.Italy;
using TestDataBuilder.Target.Entities;
using TestDataBuilder.Target.Tests.Helpers;

namespace TestDataBuilder.Target.Tests.Builder;

public class CompanyEntityBuilder : TestDataBuilder<CompanyEntity, CompanyEntityBuilder>
{
    #region Entity Properties

    private string _corporateName { get; set; }
    private string _fancyName { get; set; }
    private string _fiscalCodeNumber { get; set; }
    private DateTime? _lastUpdate { get; set; }
    private List<EmployeeEntity> _employees { get; set; } = new List<EmployeeEntity>();

    #endregion

    #region  Builders

    private EmployeeEntityBuilder _employeeEntityBuilder { get; set; }

    #endregion

    #region Flags

    private CountryOfPerson _countryOfPerson = CountryOfPerson.OTHER;

    private bool _uniqueFiscalCode = false;

    #endregion

    public CompanyEntityBuilder(AbstractBogusLocale locale) : base(locale)
    {
        _employeeEntityBuilder = new EmployeeEntityBuilder(Locale);
    }

    public CompanyEntityBuilder WithRandomCorportateName()
    {
        _corporateName = Faker.Company.CompanyName();
        return this;
    }

    public CompanyEntityBuilder WithRandomFancyName()
    {
        _fancyName = Faker.Company.CompanyName();
        return this;
    }

    public CompanyEntityBuilder WithRandomBrazilianFiscalCodeNumber()
    {
        _fiscalCodeNumber = Faker.Company.Cnpj();
        return this;
    }

    public CompanyEntityBuilder WithRandonLastUpdate()
    {
        _lastUpdate = Faker.Date.Past();
        return this;
    }

    public CompanyEntityBuilder WithUniqueFiscalCodes()
    {
        _uniqueFiscalCode = true;
        return this;
    }

    public CompanyEntityBuilder WithAllRandom()
    {
        _corporateName = Faker.Company.CompanyName();
        _fancyName = Faker.Company.CompanyName();
        _fiscalCodeNumber = GenNumber();

        _employees = _employeeEntityBuilder
            .WithAllRandom()
            .Build(Faker.Random.Int(0, 42))
            .ToList();

        return this;

        string GenNumber()
        {
            switch (Locale)
            {
                case BrazilianBogusLocale:
                    _countryOfPerson = CountryOfPerson.BRAZIL;
                    return Faker.Company.Cnpj();
                case ItalianBogusLocale:
                    _countryOfPerson = CountryOfPerson.ITALY;
                    return Faker.Company.ItalianIVA();
                default:
                    throw new NotImplementedException("Plase, add a new type of locale here.");
            }
        }
    }

    public CompanyEntityBuilder With(EmployeeEntityBuilder builder)
    {
        if (ButHasBeenFlaged)
        {
            _employees.Clear();
            ButHasBeenFlaged = false;   
        }

        _employees.Add(builder.Build());
        return this;
    }

    public CompanyEntityBuilder WithEmployees(Action<EmployeeEntityBuilder> action, int numOfEmployees = 1)
    {
        var builder = new EmployeeEntityBuilder(Locale);
        action(builder);
        _employees = builder.Build(numOfEmployees).ToList();

        return this;
    }

    public CompanyEntityBuilder WithCorporateName(string name)
    {
        _corporateName = name;
        return this;
    }

    public CompanyEntityBuilder WithFancyName(string name)
    {
        _fancyName = name;
        return this;
    }

    public CompanyEntityBuilder WithFiscalCode(string name)
    {
        _fiscalCodeNumber = name;
        return this;
    }

    public override CompanyEntityBuilder But()
    {
        ButHasBeenFlaged = true;
        return this;
    }

    public override CompanyEntity Build()
    {
        return new Faker<CompanyEntity>(Locale.ToString())
            .CustomInstantiator(f =>
                new CompanyEntity(_corporateName, _fancyName, _fiscalCodeNumber))
            .RuleFor(c => c.Code, f => Guid.NewGuid())
            .RuleFor(c => c.LastUpdate, _lastUpdate)
            .RuleFor(c => c.Employees, _employees)
            .Generate();
    }

    public override IEnumerable<CompanyEntity> Build(int num)
    {
        return new Faker<CompanyEntity>(Locale.ToString())
            .CustomInstantiator(f =>
                new CompanyEntity(GenCorporateName(f), GenFancyName(f), GenFiscalCode(f)))
            .RuleFor(c => c.Code, f => Guid.NewGuid())
            .RuleFor(c => c.LastUpdate, f => GenLastUpdate(f))
            .RuleFor(c => c.Employees, _employees)
            .Generate(num);

        string GenCorporateName(Faker f) => string.IsNullOrEmpty(_corporateName) ? _corporateName : f.Company.CompanyName();
        string GenFancyName(Faker f) => string.IsNullOrEmpty(_fancyName) ? _fancyName : f.Company.CompanyName();
        DateTime? GenLastUpdate(Faker f) => _lastUpdate is not null ? _lastUpdate : f.Date.Past();

        string GenFiscalCode(Faker f)
        {
            if (string.IsNullOrEmpty(_fiscalCodeNumber) || _uniqueFiscalCode)
                return _fiscalCodeNumber;

            return _countryOfPerson switch
            {
                CountryOfPerson.BRAZIL => f.Company.Cnpj(),
                CountryOfPerson.ITALY => f.Company.ItalianIVA(),
                _ => throw new InvalidOperationException($"{nameof(_countryOfPerson)} not defined")
            };
        }
    }

    public override CompanyEntityBuilder And() => this;


}

enum CountryOfPerson
{
    BRAZIL,
    ITALY,
    OTHER
};
