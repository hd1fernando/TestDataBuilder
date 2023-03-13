namespace TestDataBuilder.Target.Entities;

public sealed class CompanyEntity : Entity<Guid>
{
    public CompanyEntity(string corporateName, string fancyName, string fiscalCodeNumber)
    {
        CorporateName = corporateName;
        FancyName = fancyName;
        LastUpdate = null;
        FiscalCodeNumber = fiscalCodeNumber;
    }

    public string CorporateName { get; private set; }
    public string FancyName { get; private set; }
    public string FiscalCodeNumber { get; private set; }
    public DateTime? LastUpdate { get; private set; }
    public List<EmployeeEntity> Employees { get; private set; }

    public void AddEmployee(EmployeeEntity employee)
    {
        Employees.Add(employee);
    }


}
