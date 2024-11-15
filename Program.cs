using System.Text;

Console.WriteLine("Enter Absolute File Path: ");
string filePath = Console.ReadLine();
try
{
    string[] fileLines = System.IO.File.ReadAllLines(filePath);
    Parser.Parse(System.IO.File.ReadAllLines(filePath));
}
catch (Exception ex)
{
    Console.WriteLine("{0} Exception Caught: ", ex.ToString());
    return;
}

BankersAlgorithm.FindSafeOrders();

// for ease
string path = @"D:\Projects\Homework4\Homework4\Results.txt";

StringBuilder sb = new();
for (int i = 0; i < BankersAlgorithm.SuccessfulPaths.Count; i++)
{
    var line = BankersAlgorithm.SuccessfulPaths[i];
    sb.Append($"Path {i + 1}: ");
    for (int j = 0; j < line.Count; j++)
    {
        sb.Append(line[j] + " ");
    }

    sb.AppendLine();
}
File.WriteAllText(path, sb.ToString());

Console.ReadKey();