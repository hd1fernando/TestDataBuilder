namespace TestDataBuilder.Target.Tests.Helpers;

public static class BougsExtensions
{
    public static string ItalianIVA(this Bogus.DataSets.Company company)
    {
        var faker = new Faker("it");
        var companyName = faker.Company.CompanyName();
        var dateOfBirth = faker.Date.Future(-30);
        var placeOfBirth = faker.Address.City();
        return ItalianIVA(company, companyName, dateOfBirth, placeOfBirth);
    }

    public static string ItalianIVA(this Bogus.DataSets.Company _, string companyName, DateTime dateOfBirth, string placeOfBirth)
    {
        // Remove any non-letter characters from the company name and convert to upper case
        string sanitizedCompanyName = new string(companyName
            .Where(c => char.IsLetter(c))
            .ToArray())
            .ToUpper();

        // Get the day of birth as a string with leading zero, e.g. "01"
        string dayOfBirth = dateOfBirth.Day.ToString("D2");

        // Get the month of birth as a letter, e.g. "A" for January
        char monthOfBirth = (char)('A' + (dateOfBirth.Month - 1));

        // Get the year of birth as a two-digit number, e.g. "99" for 1999
        string yearOfBirth = dateOfBirth.Year.ToString().Substring(2);

        // Convert the place of birth to upper case and remove any non-letter characters
        string sanitizedPlaceOfBirth = new string(placeOfBirth
            .Where(c => char.IsLetter(c))
            .ToArray())
            .ToUpper();

        // Generate a random number between 1 and 999
        Random random = new Random();
        int randomNumber = random.Next(1, 1000);

        // Concatenate the parts of the Fiscal Code
        string fiscalCode = sanitizedCompanyName.Substring(0, Math.Min(sanitizedCompanyName.Length, 3))
            + sanitizedPlaceOfBirth.Substring(0, Math.Min(sanitizedPlaceOfBirth.Length, 3))
            + yearOfBirth
            + monthOfBirth
            + dayOfBirth
            + randomNumber.ToString("D3");

        return fiscalCode;
    }
}
