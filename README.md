# ASEAN Phonebook System (C# / .NET 8)

## Features
- Add students with no fixed student limit (`List<Student>`).
- Edit a student's details by student number.
- Search one or multiple ASEAN countries, or choose ALL.
- Display matches alphabetically by surname.
- Validate country codes (only 60, 62, 63, 65, and 66 are accepted).
- No database and no text-file storage. Records exist only while the program is running.

## Requirements
- .NET 8 SDK
- Visual Studio, Visual Studio Code, or another C# editor

## Run the program
Open a terminal in this project folder and run:

```bash
dotnet build
dotnet run
```

## Project files
- `Program.cs` – main menu, user input, and program flow.
- `Student.cs` – Student class, properties, constructor, and display methods.
- `Phonebook.cs` – stores students in a `List<Student>`, finds records, and searches by country.
- `Country.cs` – valid ASEAN country codes and country-name lookup.

## Git and GitHub workflow
Create an empty GitHub repository named `ASEANPhonebook`. In the project folder:

```bash
git init
git add .
git commit -m "Initial project structure and student model"
git branch -M main
git remote add origin YOUR_GITHUB_REPOSITORY_URL
git push -u origin main
```

Create feature branches and commit meaningful changes as you work:

```bash
git switch -c feature/phonebook-storage
git add .
git commit -m "Add student storage and duplicate checking"
git switch main
git merge feature/phonebook-storage

git switch -c feature/edit-student
git add .
git commit -m "Implement edit student menu"
git switch main
git merge feature/edit-student

git switch -c feature/country-search
git add .
git commit -m "Implement multi-country search and sorting"
git switch main
git merge feature/country-search

git push -u origin main
git push -u origin feature/phonebook-storage
git push -u origin feature/edit-student
git push -u origin feature/country-search
```

Important: Do not create fake development history by making all commits at once at the end. Make commits as you actually complete each feature. Keep the branches on GitHub so the instructor can inspect them.

## Suggested test demonstration
1. Add student `2004-56`, Lee, Sukarno, Doctor, M, country code `63`, area code `2`, number `4567890`.
2. Edit student `2004-56`, select option `5`, and change the country code to `66`.
3. Search Thailand (choice `2`). The student should appear.
4. Search Philippines (choice `1`). That student should no longer appear because the updated country code is `66`.
5. Try entering country code `99` while adding or editing. The program rejects it and asks for a valid code.
6. Choose `6` in country search to display students from all five countries.

## Notes
- The first name is collected and displayed, but the edit menu follows the assignment's listed options.
- Student number is checked for duplicates.
- Country search uses each student's current country code, including edits.
