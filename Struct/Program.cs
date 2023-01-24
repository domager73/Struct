using NewStudent;

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


static int GetIndexById(Student[] students, int id)
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

static Student CreateStudent(ref int Current_ID, bool inNewId)
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

    Console.Write("Enter the student's age: ");
    student.Age = int.Parse(Console.ReadLine());

    Console.Write("Enter Oge Points: ");
    student.Oge = int.Parse(Console.ReadLine());

    Console.Write("Enter Ege Points: ");
    student.Ege = int.Parse(Console.ReadLine());

    student.Mark = (student.Ege + student.Oge) / 2;

    return student;
}

Student CreateEmptyStudent()
{
    Student student;
    student.Id = 0;
    student.Mark = 0;
    student.Age = 0;
    student.Oge = 0;
    student.Ege = 0;
    student.Name = "";
    student.SurName = "";

    return student;

}

static void PrintStudent(Student student)
{
    Console.WriteLine("{0, -3}{1, -15}{2, -15}{3, -5}{4, -5}{5, -5}{6, -4}", student.Id, student.Name, student.SurName,
       student.Age, student.Mark, student.Oge, student.Ege);
}

static void PrintManyStudents(Student[] students)
{
    Console.WriteLine("{0, -3}{1, -15}{2, -15}{3, -5}{4, -5}{5, -5}{6, -4}", "Id", "Name", "Surname", "Age",
    "Mark", "Oge", "Ege");
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
        }
    }

    return findStudents;
}

Student[] FindStudentsFromMintoMaxEge(Student[] students, Student student, int minEge, int maxEge)
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
#endregion

#region Interfes Method

static void PrintMenu()
{
    Console.WriteLine("1. Add new student");
    Console.WriteLine("2. Delete student by id");
    Console.WriteLine("3. Clear all students");
    Console.WriteLine("4. Update student by id");
    Console.WriteLine("5. New student you position");
    Console.WriteLine("6. Print student by id");
    Console.WriteLine("7. Find student from min to max Oge");
    Console.WriteLine("8. Find student from min to max Ege");
    Console.WriteLine("9. Students who have passed the school");
    Console.WriteLine("0. Exit");
}

static int InputInt(string message)
{
    bool inputReault;
    int number;

    do
    {
        Console.WriteLine(message);
        inputReault = int.TryParse(Console.ReadLine(), out number);
    } while (!inputReault);

    return number;
}
#endregion

# region CRUD Method

static void ClearAllStudents(ref Student[] students)
{
    students = null;
}

static void updateStudentById(Student[] students, int id, Student student)
{
    int indexUpdate = GetIndexById(students, id);

    if (indexUpdate == -1)
    {
        Console.WriteLine("Dele is imposible. Element not found");
        return;
    }

    student.Id = students[indexUpdate].Id;

    students[indexUpdate] = student;
}

static void DeleteStudentById(ref Student[] students, int id)
{
    int indexDelete = GetIndexById(students, id);

    if (indexDelete == -1)
    {
        Console.WriteLine("Dele is imposible. Element not found");
        return;
    }

    Student[] newStudents = new Student[students.Length - 1];
    int newI = 0;

    for (int i = 0; i < newStudents.Length; i++)
    {
        if (i != indexDelete)
        {
            newStudents[newI] = students[i];
            newI++;
        }
    }

    students = newStudents;
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

static void InsertStudentIntoPosition(ref Student[] students, int position, Student student)
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
        case 1:
            {
                Student student = CreateStudent(ref Current_ID, true);
                AddNewStudent(ref students, student);
                continue;
            }

        case 0:
            {
                Console.WriteLine("Program will be finish");

                runProgram = false;
                break;
            }

        case 2:
            {
                int id = InputInt("Ibput id for delete: ");
                DeleteStudentById(ref students, id);
                break;
            }

        case 3:
            {
                ClearAllStudents(ref students);
                break;
            }

        case 4:
            {
                int id = InputInt("Ibput id for update: ");
                Student student = CreateStudent(ref Current_ID, false);
                updateStudentById(students, id, student);
                break;
            }
        case 5:
            {
                int position = InputInt("Input position for insert");

                Student student = CreateStudent(ref Current_ID, true);
                InsertStudentIntoPosition(ref students, position, student);
                break;
            }

        case 6:
            {
                int id = InputInt("Enter id student");
                Student student;
                bool isFinded = FindStudentById(students, id, out student);

                if (isFinded)
                {
                    PrintStudent(student);
                }
                else
                {
                    Console.WriteLine("Print is impossible. Element not fiend");
                }
                break;
            }


        case 7:
            {
                int minInOge = GetMinOge(students);

                int maxInOge = GetMaxOge(students);

                Console.WriteLine($"Input min and max Oge from {minInOge} to {maxInOge}");

                int minOge = InputInt("Min Oge: ");

                int maxOge = InputInt("Min Ege: ");

                Student[] findedStudents = FindStudentsFromMintoMaxOge(students, minOge, maxOge);

                Console.Clear();
                PrintManyStudents(findedStudents);
                break;
            }
        case 8: 
            {
                int minInEge = GetMinEge(students);

                int maxInEge = GetMaxEge(students);

                Console.WriteLine($"Input min and max Oge from {minInEge} to {maxInEge}");

                int minEge = InputInt("Min Ege: ");

                int maxEge = InputInt("Min Ege: ");

                Student[] findedStudents = FindStudentsFromMintoMaxEge(students, minEge, maxEge);

                Console.Clear();
                PrintManyStudents(findedStudents);
                break;
            }

        case 9: 
            {
                Student[] findedStudents = FindStudentLeaveSchool(students);

                Console.Clear();

                PrintManyStudents(findedStudents);

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