public static class BankersAlgorithm
{
    public static int NumberOfProcesses { get; set; }
    public static int NumberOfResources { get; set; }
    public static int[] Processes { get; set; }
    public static int[] Available { get; set; }
    public static int[,] Allocated { get; set; }
    public static int[,] Maximum { get; set; }
    public static int[,] Need { get; set; }
    public static List<List<int>> SuccessfulPaths { get; set; } = new List<List<int>>();

    public static void InitializeProcessesArray()
    {
        Processes = new int[BankersAlgorithm.NumberOfProcesses];
        for (int i = 0; i < Processes.Length; i++)
        {
            Processes[i] = i;
        }
    }

    static int[] clone(int[] x)
    {
        int[] y = new int[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            y[i] = x[i];
        }

        return y;
    }

    static int[,] clone(int[,] x)
    {
        int[,] y = new int[x.GetLength(0), x.GetLength(1)];
        for (int i = 0; i < x.GetLength(0); i++)
            for (int j = 0; j < x.GetLength(1); j++)
                y[i, j] = x[i, j];

        return y;
    }

    static void print(string title, int[,] x)
    {
        Console.WriteLine(title);
        for (int i = 0; i < x.GetLength(0); i++)
        {
            for (int j = 0; j < x.GetLength(1); j++)
            {
                Console.Write(x[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    static void print(string title, int[] x)
    {
        Console.WriteLine(title);
        x.ToList().ForEach(x => Console.Write(x + " "));
        Console.WriteLine();
        Console.WriteLine();
    }

    public static void FindSafeOrders()
    {
        calculateNeed();
        print("Processes", Processes);
        print("Available", Available);
        print("Allocated", Allocated);
        print("Maximum", Maximum);
        print("Need", Need);
        findSafeOrdersRecursive(new List<int>(), clone(Processes), clone(Available), clone(Allocated), clone(Maximum), clone(Need));
        SuccessfulPaths.ForEach(x => { x.ForEach(y => Console.Write(y + " ")); });
        for (int i = 0; i < SuccessfulPaths.Count; i++)
        {
            var path = SuccessfulPaths[i];
            Console.Write($"Path {i}: ");
            path.ToList().ForEach(x => Console.Write($"P{x+1} "));
            Console.WriteLine();
        }

        Console.WriteLine($"Finished. Found {SuccessfulPaths.Count} total paths.");
    }

    static void findSafeOrdersRecursive(List<int> completedProcesses, int[] remainingProcesses, int[] available, int[,] allocated, int[,] maximum, int[,] need)
    {
        if (!remainingProcesses.Any())
        {
            SuccessfulPaths.Add(completedProcesses);
            return;
        }

        foreach (int p in remainingProcesses)
        {
            if (canRun(p, need, available, allocated))
            {
                var c = new List<int>(completedProcesses);
                c.Add(p);
                findSafeOrdersRecursive(c, remainingProcesses.Where(x => x != p).ToArray(),
                    clone(available), clone(allocated), clone(maximum), clone(need));
            }
        }
    }

    static bool canRun(int p, int[,] need, int[] available, int[,] allocated)
    {
        for (int i = 0; i < NumberOfResources; i++)
            if (need[p, i] > available[i])
                return false;

        // Add the allocated resources of
        // current P to the available/work
        // resources i.e.free the resources
        for (int i = 0; i < NumberOfResources; i++)
            available[i] += allocated[p, i];
        
        return true;
    }

    // Function to find the need of each process
    static void calculateNeed()
    {
        Need = new int[NumberOfProcesses, NumberOfResources];
        // Calculating Need of each P
        for (int i = 0; i < NumberOfProcesses; i++)
            for (int j = 0; j < NumberOfResources; j++)
                Need[i, j] = Maximum[i, j] - Allocated[i, j];
    }
}