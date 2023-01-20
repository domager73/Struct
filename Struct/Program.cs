using NewProduct;
using System.Collections.Immutable;

int Current_ID = 0;

# region System Methonds
static void ResizeArray(ref Product[] products, int newLength)
{
    int minLenght = newLength > products.Length ? products.Length : newLength;

    Product[] newArray = new Product[newLength];

    for (int i = 0; i < minLenght; i++)
    {
        newArray[i] = products[i];
    }

    products = newArray;
}

# endregion 

#region tools Methods

static int GetIndexById(Product[] products, int id)
{
    if (products == null)
    {
        return -1;
    }
    for (int i = 0; i < products.Length; i++)
    {
        if (products[i].Id == id)
        {
            return i;
        }
    }
    return -1;
}

static Product CreateProduct(ref int Current_ID, bool inNewId)
{
    Product product;

    if (inNewId)
    {
        product.Id = Current_ID;
    }
    else 
    {
        product.Id = 0;
    }

    Console.Write("Введите название продукта: ");
    product.Name = Console.ReadLine();

    Console.Write("Введите поставщика: ");
    product.Contractor = Console.ReadLine();

    Console.Write("Введите дату поставки: ");
    product.DeliveryDate = DateTime.Parse(Console.ReadLine());

    Console.Write("Введите срок годности: ");
    product.SelfLifesDays = int.Parse(Console.ReadLine());

    Console.Write("Введите остаток продукта: ");
    product.Balanse = int.Parse(Console.ReadLine());

    Console.Write("Введите цену: ");
    product.Prise = int.Parse(Console.ReadLine());

    return product;
}

static void PrintProduct(Product product)
{
    Console.WriteLine("{0, -3}{1, -15}{2, -15}{3, -12}{4, -4}{5, -4}{6, -4}", product.Id, product.Name, product.Contractor,
        product.DeliveryDate.ToShortDateString(), product.SelfLifesDays, product.Balanse, product.Prise);
}

static void PrintManyRpoducts(Product[] products)
{
    Console.WriteLine("{0, -3}{1, -15}{2, -15}{3, -12}{4, -4}{5, -4}{6, -4}", "Id", "Название", "Почтавщик", "Дата дост",
    "СГ", "Ост", "Цена(руб)");
    if (products == null)
    {
        Console.WriteLine("Array is empty");
    }
    else if (products.Length == 0)
    {
        Console.WriteLine("Array is empty");
    }
    else
    {
        for (int i = 0; i < products.Length; i++)
        {
            PrintProduct(products[i]);
        }
    }
    Console.WriteLine("-------------------");
}
#endregion

#region Interfes Method

static void PrintMenu()
{
    Console.WriteLine("1. Add new product");
    Console.WriteLine("2. Delete product by id");
    Console.WriteLine("3. Clear all products");
    Console.WriteLine("4. Update product by id");
    Console.WriteLine("5. New product you position");
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

static void ClearAllProducts(ref Product[] products)
{
    products = null;
}

static void updateProductById(Product[] products, int id, Product product)
{
    int indexUpdate = GetIndexById(products, id);

    if (indexUpdate == -1)
    {
        Console.WriteLine("Dele is imposible. Element not found");
        return;
    }

    product.Id = products[indexUpdate].Id;

    products[indexUpdate] = product;
}

static void DeleteProductById(ref Product[] products, int id)
{
    int indexDelete = GetIndexById(products, id);

    if (indexDelete == -1)
    {
        Console.WriteLine("Dele is imposible. Element not found");
        return;
    }

    Product[] newProducts = new Product[products.Length - 1];
    int newI = 0;

    for (int i = 0; i < newProducts.Length; i++)
    {
        if (i != indexDelete)
        {
            newProducts[newI] = products[i];
            newI++;
        }
    }

    products = newProducts;
}

void AddNewProduct(ref Product[] products, Product product)
{
    Current_ID++;
    product.Id = Current_ID;

    if (products == null)
    {
        products = new Product[1];
    }
    else
    {
        ResizeArray(ref products, products.Length + 1);
    }
    products[products.Length - 1] = product;
}

static void InsertProductIntoPosition(ref Product[] products, int position, Product product) 
{
    if (products == null) 
    {
        Console.WriteLine("Insert is imposible. Array is found");
        return;
    }

    if (position < 1 || position > products.Length) 
    {
        Console.WriteLine("Insert is imposible. Position not found");
    }

    int indexinsert = position - 1;

    Product[] newProducts = new Product[products.Length + 1];

    int oldI = 0;

    for (int i = 0; i < newProducts.Length; i++) 
    {
        if (i != indexinsert)
        {
            newProducts[i] = products[oldI];
            oldI++;
            products[i].Id++;
        }
        else 
        {
            newProducts[i] = product;    
        }
    }
    products = newProducts;
}
# endregion

Product[] products = null;
bool runProgram = true;

while (runProgram)
{
    Console.Clear();
    PrintManyRpoducts(products);

    PrintMenu();
    int menuPoint = InputInt("Input menu point: ");

    switch (menuPoint)
    {
        case 1:
            {
                Product product = CreateProduct(ref Current_ID, true);
                AddNewProduct(ref products, product);
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
                DeleteProductById(ref products, id);
                break;
            }

        case 3:
            {
                ClearAllProducts(ref products);
                break;
            }

        case 4:
            {
                int id = InputInt("Ibput id for update: ");
                Product product = CreateProduct(ref Current_ID, false);
                updateProductById(products, id, product);
                break;
            }
        case 5:
            {
                int position = InputInt("Input position for insert");

                Product product = CreateProduct(ref Current_ID, true);
                InsertProductIntoPosition(ref products, position, product);
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