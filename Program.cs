using System;
using System.Collections.Generic;

namespace ASEANPhonebook;

internal class Program
{
    private static readonly Phonebook phonebook = new();

    private static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== ASEAN PHONEBOOK ==========");
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    StoreStudents();
                    break;
                case "2":
                    EditStudent();
                    break;
                case "3":
                    SearchStudentsByCountry();
                    break;
                case "4":
                    running = false;
                    Console.WriteLine("Thank you for using ASEAN Phonebook!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose 1 to 4.");
                    break;
            }
        }
    }

    private static void StoreStudents()
    {
        bool addAnother = true;

        while (addAnother)
        {
            Console.WriteLine("\n--- STORE TO ASEAN PHONEBOOK ---");

            string studentNumber = ReadRequired("Enter student number: ");

            if (phonebook.HasStudentNumber(studentNumber))
            {
                Console.WriteLine("That student number already exists. Please use a unique number.");
                continue;
            }

            string surname = ReadRequired("Enter surname: ");
            string firstName = ReadRequired("Enter first name: ");
            string occupation = ReadRequired("Enter occupation: ");
            string gender = ReadGender();
            string countryCode = ReadCountryCode();
            string areaCode = ReadRequired("Enter area code: ");
            string phoneNumber = ReadRequired("Enter number: ");

            Student newStudent = new(
                studentNumber, surname, firstName, occupation,
                gender, countryCode, areaCode, phoneNumber);

            if (phonebook.AddStudent(newStudent))
            {
                Console.WriteLine("Student successfully added!");
            }
            else
            {
                Console.WriteLine("Unable to add student. Student number already exists.");
            }

            addAnother = ReadYesNo("Do you want to enter another entry [Y/N]? ");
        }
    }

    private static void EditStudent()
    {
        Console.WriteLine("\n--- EDIT ENTRY IN ASEAN PHONEBOOK ---");
        string studentNumber = ReadRequired("Enter student number: ");

        Student? student = phonebook.FindStudent(studentNumber);

        if (student == null)
        {
            Console.WriteLine("Error: Student number does not exist.");
            return;
        }

        bool editing = true;

        while (editing)
        {
            phonebook.DisplayEditInformation(student);

            Console.WriteLine("\nWhich information do you wish to change?");
            Console.WriteLine("[1] Student number  [2] Surname  [3] Gender  [4] Occupation");
            Console.WriteLine("[5] Country code    [6] Area code [7] Phone number");
            Console.WriteLine("[8] None - Go back to main menu");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    string newStudentNumber = ReadRequired("Enter new student number: ");
                    if (newStudentNumber.Equals(student.StudentNumber, StringComparison.OrdinalIgnoreCase)
                        || !phonebook.HasStudentNumber(newStudentNumber))
                    {
                        student.StudentNumber = newStudentNumber;
                        Console.WriteLine("Student number updated.");
                    }
                    else
                    {
                        Console.WriteLine("That student number is already used by another student.");
                    }
                    break;

                case "2":
                    student.Surname = ReadRequired("Enter new surname: ");
                    Console.WriteLine("Surname updated.");
                    break;

                case "3":
                    student.Gender = ReadGender();
                    Console.WriteLine("Gender updated.");
                    break;

                case "4":
                    student.Occupation = ReadRequired("Enter new occupation: ");
                    Console.WriteLine("Occupation updated.");
                    break;

                case "5":
                    student.CountryCode = ReadCountryCode();
                    Console.WriteLine("Country code updated.");
                    break;

                case "6":
                    student.AreaCode = ReadRequired("Enter new area code: ");
                    Console.WriteLine("Area code updated.");
                    break;

                case "7":
                    student.PhoneNumber = ReadRequired("Enter new phone number: ");
                    Console.WriteLine("Phone number updated.");
                    break;

                case "8":
                    editing = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please choose 1 to 8.");
                    break;
            }
        }
    }

    private static void SearchStudentsByCountry()
    {
        Console.WriteLine("\n--- SEARCH ASEAN PHONEBOOK BY COUNTRY ---");
        Console.WriteLine("From which country?");
        Country.DisplayCountryChoices();
        Console.WriteLine("[6] ALL (all countries)");
        Console.WriteLine("[0] No More");

        HashSet<string> selectedCodes = new();
        bool selecting = true;
        bool allCountries = false;

        while (selecting)
        {
            Console.Write("Enter country choice: ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input. Enter a number from 0 to 6.");
                continue;
            }

            if (choice == 0)
            {
                selecting = false;
            }
            else if (choice == 6)
            {
                allCountries = true;
                selecting = false;
            }
            else
            {
                string code = Country.GetCodeFromChoice(choice);

                if (code == "")
                {
                    Console.WriteLine("Invalid country choice. Enter 0 to 6.");
                }
                else
                {
                    selectedCodes.Add(code);
                    Console.WriteLine($"{Country.GetName(code)} selected.");
                }
            }
        }

        if (allCountries)
        {
            selectedCodes = new HashSet<string> { "60", "62", "63", "65", "66" };
        }

        if (selectedCodes.Count == 0)
        {
            Console.WriteLine("No country selected. Returning to main menu.");
            return;
        }

        phonebook.SearchByCountries(selectedCodes);
    }

    private static string ReadCountryCode()
    {
        while (true)
        {
            Console.Write("Enter country code (60, 62, 63, 65, 66): ");
            string code = Console.ReadLine()?.Trim() ?? "";

            if (Country.IsValidCode(code))
            {
                return code;
            }

            Console.WriteLine("Invalid country code. Code 99 is not accepted. Please try again.");
        }
    }

    private static string ReadGender()
    {
        while (true)
        {
            Console.Write("Enter gender (M for male, F for female): ");
            string gender = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (gender == "M" || gender == "F")
            {
                return gender;
            }

            Console.WriteLine("Invalid gender. Please enter M or F.");
        }
    }

    private static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string value = Console.ReadLine()?.Trim() ?? "";

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }

    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string answer = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (answer == "Y") return true;
            if (answer == "N") return false;

            Console.WriteLine("Please enter Y or N.");
        }
    }
}
