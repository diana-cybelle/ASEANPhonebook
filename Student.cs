using System;

namespace ASEANPhonebook;

public class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public string Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(
        string studentNumber,
        string surname,
        string firstName,
        string occupation,
        string gender,
        string countryCode,
        string areaCode,
        string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = gender;
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    public string GetFullName()
    {
        return $"{FirstName} {Surname}";
    }

    public string GetInternationalNumber()
    {
        return $"{CountryCode}-{AreaCode}-{PhoneNumber}";
    }

    public void DisplayInformation()
    {
        Console.WriteLine(
            $"{GetFullName()}, student number {StudentNumber}, " +
            $"is a {Occupation}. Country: {Country.GetName(CountryCode)}. " +
            $"Phone number: {GetInternationalNumber()}");
    }
}
