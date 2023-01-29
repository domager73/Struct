using NewStudent;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

int Current_ID = 0;

# region System Methonds
static void ResizeArray(ref Student[] students, int newLength)
{
    int minLenght = newLength > students.Length ? students.Length : newLength;

    Student[] newArray = new Student[newLength];

    for (int i = 0; i < minLenght; i++)
    {
        newArray[i] = students[i];
    }

    students = newArray;
}

Student[] ReadManyStudentsFromFile(Student[] students1, string nameFile, Student[] students = null)
{
    StreamReader reader = new StreamReader(nameFile);

    int newId = 0;

    int countProducts = int.Parse(reader.ReadLine());
    Current_ID = int.Parse(reader.ReadLine());

    if (students1 == null)
    {
        students = new Student[countProducts];

        for (int i = 0; i < countProducts; i++)
        {
            students[i].Id = i + 1;
            students[i].Name = reader.ReadLine();
            students[i].SurName = reader.ReadLine();
            students[i].Class = int.Parse(reader.ReadLine());
            students[i].Oge = int.Parse(reader.ReadLine());
            students[i].Ege = int.Parse(reader.ReadLine());
            students[i].AverageScore = (students[i].Oge + students[i].Ege) / 2;
        }
    }
    else
    {
        students = new Student[countProducts + students1.Length];

        for (int i = 0; i < students1.Length; i++)
        {
            students[i].Id = newId + 1;
            students[i].Name = students1[i].Name;
            students[i].SurName = students1[i].SurName;
            students[i].Class = students1[i].Class;
            students[i].Oge = students1[i].Oge;
            students[i].Ege = students1[i].Ege;
            students[i].AverageScore = (students1[i].Oge + students1[i].Ege) / 2;

            newId++;
        }

        for (int i = students1.Length; i < countProducts + students1.Length; i++)
        {
            students[newId].Id = newId + 1;
            students[newId].Name = reader.ReadLine();
            students[newId].SurName = reader.ReadLine();
            students[newId].Class = int.Parse(reader.ReadLine());
            students[newId].Oge = int.Parse(reader.ReadLine());
            students[newId].Ege = int.Parse(reader.ReadLine());
            students[newId].AverageScore = (students[newId].Oge + students[newId].Ege) / 2;

            newId++;
        }
    }
    reader.Close();

    return students;
}

void SaveManyStudentsToFile(Student[] students, string fileName, int Current_ID)
{
    StreamWriter write = new StreamWriter(fileName);

    write.WriteLine(students.Length);
    write.WriteLine(Current_ID);

    for (int i = 0; i < students.Length; i++)
    {
        write.WriteLine(students[i].Name);
        write.WriteLine(students[i].SurName);
        write.WriteLine(students[i].Class);
        write.WriteLine(students[i].Oge);
        write.WriteLine(students[i].Ege);
    }

    write.Close();
}
# endregion 

#region tools Methods

int GetMinOge(Student[] students)
{
    int min = 101;
    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Oge < min)
        {
            min = students[i].Oge;
        }
    }

    return min;
}

int GetMaxOge(Student[] students)
{
    int max = 0;
    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Oge > max)
        {
            max = students[i].Oge;
        }
    }

    return max;
}

int GetMinEge(Student[] students)
{
    int min = 101;
    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Oge < min)
        {
            min = students[i].Ege;
        }
    }

    return min;
}

int GetMaxEge(Student[] students)
{
    int max = 0;
    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Oge > max)
        {
            max = students[i].Ege;
        }
    }

    return max;
}


int GetIndexById(Student[] students, int id)
{
    if (students == null)
    {
        return -1;
    }
    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Id == id)
        {
            return i;
        }
    }
    return -1;
}

Student CreateStudent(ref int Current_ID, bool inNewId)
{
    Student student;

    if (inNewId)
    {
        student.Id = Current_ID;
    }
    else
    {
        student.Id = 0;
    }

    Console.Write("Enter the student's name : ");
    student.Name = Console.ReadLine();

    Console.Write("Enter the student's surname : ");
    student.SurName = Console.ReadLine();

    student.Class = InputInt("Enter the student's class: ");

    student.Oge = InputInt("Enter Oge Points: ");

    student.Ege = InputInt("Enter Ege Points: ");

    student.AverageScore = (student.Ege + student.Oge) / 2;

    return student;
}

Student CreateEmptyStudent()
{
    Student student;
    student.Id = 0;
    student.AverageScore = 0;
    student.Class = 0;
    student.Oge = 0;
    student.Ege = 0;
    student.Name = "";
    student.SurName = "";

    return student;

}

void PrintStudent(Student student)
{
    Console.WriteLine("{0, -3}{1, -15}{2, -15}{3, -5}{4, -5}{5, -5}{6, -10}", student.Id, student.Name, student.SurName,
       student.Class, student.Oge, student.Ege, student.AverageScore);
}

void PrintManyStudents(Student[] students)
{
    Console.WriteLine("{0, -3}{1, -15}{2, -15}{3, -5}{4, -5}{5, -5}{6, -4}", "Id", "Name", "Surname", "Age", "Oge", "Ege",
    "AverageScore");
    if (students == null)
    {
        Console.WriteLine("Array is empty");
    }
    else if (students.Length == 0)
    {
        Console.WriteLine("Array is empty");
    }
    else
    {
        for (int i = 0; i < students.Length; i++)
        {
            PrintStudent(students[i]);
        }
    }
    Console.WriteLine("-------------------");
}
void PrintManyStudentsToFile(Student[] students, string fileName)
{
    StreamWriter writer = new StreamWriter(fileName);

    writer.WriteLine("{0, -3}{1, -15}{2, -15}{3, -5}{4, -5}{5, -5}{6, -4}", "Id", "Name", "Surname", "Class", "Oge", "Ege",
    "AverageScore");
    if (students == null)
    {
        writer.WriteLine("Array is empty");
    }
    else if (students.Length == 0)
    {
        writer.WriteLine("Array is empty");
    }
    else
    {
        for (int i = 0; i < students.Length; i++)
        {
            writer.WriteLine("{0, -3}{1, -15}{2, -15}{3, -5}{4, -5}{5, -5}{6, -10}", i + 1, students[i].Name, students[i].SurName,
                                students[i].Class, students[i].Oge, students[i].Ege, students[i].AverageScore);
        }
    }

    writer.Close();
}

    bool FindStudentById(Student[] students, int id, out Student student)
{
    int indexPrint = GetIndexById(students, id);

    if (indexPrint == -1)
    {
        student = CreateEmptyStudent();
        return false;
    }
    else
    {
        student = students[indexPrint];
        return true;
    }
}

Student[] FindStudentsFromMintoMaxOge(Student[] students, int minOge, int maxOge)
{
    Student[] findStudents = null;

    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Oge >= minOge && students[i].Oge <= maxOge)
        {
            AddNewStudent(ref findStudents, students[i]);
        }
    }

    return findStudents;
}

Student[] FindStudentLeaveSchool(Student[] students)
{
    Student[] findStudents = null;

    int minPassingScore = 30;

    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Oge > minPassingScore)
        {
            AddNewStudent(ref findStudents, students[i]);

            Current_ID--;
        }
    }

    return findStudents;
}

Student[] FindStudentsFromMintoMaxEge(Student[] students, int minEge, int maxEge)
{
    Student[] findStudents = null;

    for (int i = 0; i < students.Length; i++)
    {
        if (students[i].Oge >= minEge && students[i].Oge <= maxEge)
        {
            AddNewStudent(ref findStudents, students[i]);
        }
    }

    return findStudents;
}

bool CheckEmptyMas(Student[] students)
{
    if (students == null)
    {
        return false;
    }
    else
    {
        return true;
    }
}

void SortStudentsByAverageScore(Student[] students, bool asc)
{
    Student temp;
    bool sort;
    int offset = 0;
    int idCount = 1;

    do
    {
        sort = true;

        for (int i = 0; i < students.Length - 1 - offset; i++)
        {
            bool compareResult;

            if (asc)
            {
                compareResult = students[i + 1].AverageScore < students[i].AverageScore;
            }
            else
            {
                compareResult = students[i + 1].AverageScore > students[i].AverageScore;
            }

            if (compareResult)
            {
                temp = students[i];
                students[i] = students[i + 1];
                students[i + 1] = temp;

                idCount++;
                sort = false;
            }
        }

        offset++;

    } while (!sort);
}

void SortStudentsByID(Student[] students)
{
    Student temp;
    bool sort;
    int offset = 0;
    int idCount = 1;

    do
    {
        sort = true;

        for (int i = 0; i < students.Length - 1 - offset; i++)
        {
            bool compareResult;

            if (compareResult = students[i + 1].Id < students[i].Id)
            {
                temp = students[i];
                students[i] = students[i + 1];
                students[i + 1] = temp;

                idCount++;
                sort = false;
            }
        }

        offset++;

    } while (!sort);
}

void SortStudentsByClass(Student[] students, bool asc)
{
    Student temp;
    bool sort;
    int offset = 0;
    int idCount = 1;

    do
    {
        sort = true;

        for (int i = 0; i < students.Length - 1 - offset; i++)
        {
            bool compareResult;

            if (asc)
            {
                compareResult = students[i + 1].Class < students[i].Class;
            }
            else
            {
                compareResult = students[i + 1].Class > students[i].Class;
            }

            if (compareResult)
            {
                temp = students[i];
                students[i] = students[i + 1];
                students[i + 1] = temp;

                idCount++;
                sort = false;
            }
        }

        offset++;

    } while (!sort);
}


#endregion

#region Interfes Method

void PrintMenu()
{
    Console.WriteLine("1. Add new student");
    Console.WriteLine("2. Clear all students");
    Console.WriteLine("3. Update student by id");
    Console.WriteLine("4. New student you position");
    Console.WriteLine("5. Print student by id");
    Console.WriteLine("6. Sort students");
    Console.WriteLine("7. Search from students");
    Console.WriteLine("8. Print students to txt file");
    Console.WriteLine("9. Save students to data file");
    Console.WriteLine("10. Read students to data file");
    Console.WriteLine("0. Exit");
}

void PrintSearchMenu()
{
    Console.WriteLine("1. Find student from min to max Oge");
    Console.WriteLine("2. Find student from min to max Ege");
    Console.WriteLine("3. Students who have passed the school");
    Console.WriteLine("0. Exit");
}

void PrintSortMenu()
{
    Console.WriteLine("1. Sort by ascending average score");
    Console.WriteLine("2. Sort by decreasing average score");
    Console.WriteLine("3. Sort by ascending class");
    Console.WriteLine("4. Sort by decreasing class");
    Console.WriteLine("5. Bring everything back");
    Console.WriteLine("0. Exit");
}

int InputInt(string message)
{
    bool inputReault;
    int number;

    do
    {
        Console.Write(message);
        inputReault = int.TryParse(Console.ReadLine(), out number);
    } while (!inputReault);

    return number;
}

#endregion

# region CRUD Method

void ClearAllStudents(ref Student[] students)
{
    students = null;
}

void updateStudentById(Student[] students, int id, Student student)
{
    int indexUpdate = GetIndexById(students, id);

    if (indexUpdate == -1)
    {
        Console.WriteLine("Delete is imposible. Element not found");
        return;
    }

    student.Id = students[indexUpdate].Id;

    students[indexUpdate] = student;
}

void AddNewStudent(ref Student[] students, Student student)
{
    Current_ID++;
    student.Id = Current_ID;

    if (students == null)
    {
        students = new Student[1];
    }
    else
    {
        ResizeArray(ref students, students.Length + 1);
    }
    students[students.Length - 1] = student;
}

void InsertStudentIntoPosition(ref Student[] students, int position, Student student)
{
    if (students == null)
    {
        Console.WriteLine("Insert is imposible. Array is found");
        return;
    }

    if (position < 1 || position > students.Length)
    {
        Console.WriteLine("Insert is imposible. Position not found");
    }

    int indexinsert = position - 1;

    Student[] newStudents = new Student[students.Length + 1];

    int oldI = 0;

    for (int i = 0; i < newStudents.Length; i++)
    {
        if (i != indexinsert)
        {
            newStudents[i] = students[oldI];
            oldI++;
            students[i].Id++;
        }
        else
        {
            newStudents[i] = student;
        }
    }
    students = newStudents;
}
# endregion

Student[] students = null;
bool runProgram = true;

while (runProgram)
{
    Console.Clear();
    PrintManyStudents(students);

    PrintMenu();
    int menuPoint = InputInt("Input menu point: ");

    switch (menuPoint)
    {
        case 0:
            {
                Console.WriteLine("Program will be finish");

                runProgram = false;
                break;
            }

        case 1:
            {
                Student student = CreateStudent(ref Current_ID, true);
                AddNewStudent(ref students, student);
                continue;
            }

        case 2:
            {
                if (CheckEmptyMas(students))
                {
                    ClearAllStudents(ref students);
                }
                else
                {
                    Console.WriteLine("Already an empty database");
                }

                break;
            }

        case 3:
            {
                if (CheckEmptyMas(students))
                {
                    int id = InputInt("Ibput id for update: ");
                    Student student = CreateStudent(ref Current_ID, false);

                    updateStudentById(students, id, student);
                }
                else
                {
                    Console.WriteLine("Empty database");
                }
                break;
            }
        case 4:
            {
                if (CheckEmptyMas(students))
                {
                    int position = InputInt("Input position for insert: ");

                    Student student = CreateStudent(ref Current_ID, true);
                    InsertStudentIntoPosition(ref students, position, student);
                    break;
                }
                else
                {
                    Console.WriteLine("Empty database");
                }

                break;
            }

        case 5:
            {
                if (CheckEmptyMas(students))
                {
                    int id = InputInt("Enter id student: ");
                    Student student;
                    bool isFinded = FindStudentById(students, id, out student);

                    if (isFinded)
                    {
                        Console.WriteLine("{0, -3}{1, -15}{2, -15}{3, -5}{4, -5}{5, -5}{6, -4}", "Id", "Name", "Surname", "Class", "Oge", "Ege", "AverageScore");
                        PrintStudent(student);
                    }
                    else
                    {
                        Console.WriteLine("Print is impossible. Element not fiend");
                    }
                }
                else
                {
                    Console.WriteLine("Empty database");
                }

                break;
            }

        case 6:
            {
                if (CheckEmptyMas(students))
                {
                    bool runSortMenu = true;

                    while (runSortMenu)
                    {
                        Console.Clear();
                        PrintManyStudents(students);

                        PrintSortMenu();
                        int menuSort = InputInt("Input sort point: ");

                        switch (menuSort)
                        {
                            case 0:
                                {
                                    Console.WriteLine("Exit the search menu ");

                                    runSortMenu = false;
                                    break;
                                }
                            case 1:
                                {
                                    SortStudentsByAverageScore(students, true);

                                    break;
                                }

                            case 2:
                                {
                                    SortStudentsByAverageScore(students, false);

                                    break;
                                }
                            case 3:
                                {
                                    SortStudentsByID(students);

                                    break;
                                }

                            case 4:
                                {
                                    SortStudentsByClass(students, true);

                                    break;
                                }

                            case 5:
                                {
                                    SortStudentsByClass(students, false);

                                    break;
                                }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Empty database");
                }
                break;
            }

        case 7:
            {
                if (CheckEmptyMas(students))
                {
                    bool runSearchMenu = true;

                    while (runSearchMenu)
                    {
                        Console.Clear();
                        PrintManyStudents(students);

                        PrintSearchMenu();
                        int menuSearch = InputInt("Input search point: ");

                        switch (menuSearch)
                        {
                            case 0:
                                {
                                    Console.WriteLine("Exit the search menu ");

                                    runSearchMenu = false;
                                    break;
                                }

                            case 1:
                                {
                                    if (CheckEmptyMas(students))
                                    {
                                        int minInOge = GetMinOge(students);

                                        int maxInOge = GetMaxOge(students);

                                        Console.WriteLine($"Input min and max Oge from {minInOge} to {maxInOge}");

                                        int minOge = InputInt("Min Oge: ");

                                        int maxOge = InputInt("Min Ege: ");

                                        Student[] findedStudents = FindStudentsFromMintoMaxOge(students, minOge, maxOge);

                                        Console.Clear();
                                        PrintManyStudents(findedStudents);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty database");
                                    }

                                    Console.ReadLine();

                                    break;
                                }
                            case 2:
                                {
                                    if (CheckEmptyMas(students))
                                    {
                                        int minInEge = GetMinEge(students);

                                        int maxInEge = GetMaxEge(students);

                                        Console.WriteLine($"Input min and max Oge from {minInEge} to {maxInEge}");

                                        int minEge = InputInt("Min Ege: ");

                                        int maxEge = InputInt("Min Ege: ");

                                        Student[] findedStudents = FindStudentsFromMintoMaxEge(students, minEge, maxEge);

                                        Console.Clear();
                                        PrintManyStudents(findedStudents);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty database");
                                    }

                                    Console.ReadLine();

                                    break;
                                }

                            case 3:
                                {
                                    if (CheckEmptyMas(students))
                                    {
                                        Student[] findedStudents = FindStudentLeaveSchool(students);

                                        Console.Clear();

                                        PrintManyStudents(findedStudents);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty database");
                                    }

                                    Console.ReadLine();

                                    break;
                                }

                            default:
                                {
                                    Console.WriteLine("Unknown command");
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Empty database");
                }
                break;
            }

        case 8:
            {
                Console.Write("Iput file name: ");

                PrintManyStudentsToFile(students, Console.ReadLine());
                break;
            }
        case 9: 
            {
                Console.Write("Iput file name: ");

                SaveManyStudentsToFile(students, Console.ReadLine(), Current_ID);
                break;
            }

        case 10:
            {
                Console.Write("Iput file name: ");
                string fileName = Console.ReadLine();

                students = ReadManyStudentsFromFile(students ,fileName);
                break;
            }

        default:
            {
                Console.WriteLine("Unknown command");
                break;
            }
    }
    Console.ReadKey();
}

Console.ReadKey();