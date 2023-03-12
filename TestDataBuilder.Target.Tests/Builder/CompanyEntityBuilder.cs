using Bogus;
using Bogus.Extensions.Brazil;
using TestDataBuilder.Target.Entities;

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

    public CompanyEntityBuilder(string locale) : base(locale) { }

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

    public CompanyEntityBuilder WithRandolastUpdate()
    {
        _lastUpdate = Faker.Date.Soon();
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

    public CompanyEntityBuilder WithEmployee(EmployeeEntity employee)
    {
        _employees.Add(employee);
        return this;
    }

    public override CompanyEntityBuilder But() => this;

    public override CompanyEntity Build()
    {
        return new Faker<CompanyEntity>(Locale)
            .CustomInstantiator(f =>
                new CompanyEntity(_corporateName, _fancyName, _fiscalCodeNumber))
            .RuleFor(c => c.Code, f => Guid.NewGuid())
            .RuleFor(c => c.LastUpdate, _lastUpdate)
            .RuleFor(c => c.Employees, _employees)
            .Generate();
    }

    public override IEnumerable<CompanyEntity> Build(int num)
    {
        return new Faker<CompanyEntity>(Locale)
            .CustomInstantiator(f =>
                new CompanyEntity(GenCorporateName(f), GenFancyName(f), GenFiscalCode(f)))
            .RuleFor(c => c.Code, f => Guid.NewGuid())
            .RuleFor(c => c.LastUpdate, f => GenLastUpdate(f))
            .RuleFor(c => c.Employees, _employees)
            .Generate(num);

        string GenCorporateName(Faker f) => string.IsNullOrEmpty(_corporateName) ? _corporateName : f.Company.CompanyName();
        string GenFancyName(Faker f) => string.IsNullOrEmpty(_fancyName) ? _fancyName : f.Company.CompanyName();
        string GenFiscalCode(Faker f) => string.IsNullOrEmpty(_fiscalCodeNumber) ? _fiscalCodeNumber : f.Company.Cnpj();
        DateTime? GenLastUpdate(Faker f) => _lastUpdate is not null ? _lastUpdate : f.Date.Soon();
    }

}
