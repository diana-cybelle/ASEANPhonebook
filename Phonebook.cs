using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEANPhonebook;

public class Phonebook
{
    // List can grow as students are added. It has no fixed student limit.
    private readonly List<Student> students = new();

    public bool AddStudent(Student student)
    {
        if (FindStudent(student.StudentNumber) != null)
        {
            return false;
        }

        students.Add(student);
        return true;
    }

    public Student? FindStudent(string studentNumber)
    {
        return students.FirstOrDefault(
            student => student.StudentNumber.Equals(
                studentNumber, StringComparison.OrdinalIgnoreCase));
    }

    public bool HasStudentNumber(string studentNumber)
    {
        return FindStudent(studentNumber) != null;
    }

    public void DisplayEditInformation(Student student)
    {
        Console.WriteLine();
        Console.WriteLine($"Here is the existing information about {student.StudentNumber}:");
        Console.WriteLine($"{student.GetFullName()} is a {student.Occupation}.");
        Console.WriteLine($"Gender: {student.Gender}");
        Console.WriteLine($"Country: {Country.GetName(student.CountryCode)} ({student.CountryCode})");
        Console.WriteLine($"Phone number: {student.GetInternationalNumber()}");
    }

    public void SearchByCountries(HashSet<string> selectedCodes)
    {
        IEnumerable<Student> matches = students
            .Where(student => selectedCodes.Contains(student.CountryCode))
            .OrderBy(student => student.Surname, StringComparer.OrdinalIgnoreCase)
            .ThenBy(student => student.FirstName, StringComparer.OrdinalIgnoreCase);

        List<Student> result = matches.ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("No students found for the selected country/countries.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Matching students (alphabetical by surname):");
        foreach (Student student in result)
        {
            student.DisplayInformation();
        }
    }

    public int Count
    {
        get { return students.Count; }
    }
}
