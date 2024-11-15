public static class Parser
{

    public static void Parse(string[] fileLines)
    {
        GetNumberOfProcesses(fileLines);
        GetNumberOfResources(fileLines);
        GetAvailable(fileLines);
        BankersAlgorithm.InitializeProcessesArray();
        GetAllocated(fileLines);
        GetMaximum(fileLines);
    }

    public static void GetNumberOfProcesses(string[] lines)
    {
        BankersAlgorithm.NumberOfProcesses = Convert.ToInt32(lines[0].Substring(lines[0].IndexOf(": ") + 2).Trim());
    }

    public static void GetNumberOfResources(string[] lines)
    {
        BankersAlgorithm.NumberOfResources = Convert.ToInt32(lines[1].Substring(lines[1].IndexOf(": ") + 2).Trim());
    }

    public static void GetAvailable(string[] lines)
    {
        BankersAlgorithm.Available = new int[BankersAlgorithm.NumberOfResources];

        string[] nums = lines[4].Split(",");
        for (int i = 0; i < BankersAlgorithm.NumberOfResources; i++)
        {
            BankersAlgorithm.Available[i] = Convert.ToInt32(nums[i]);
        }
    }

    public static void GetAllocated(string[] lines)
    {
        BankersAlgorithm.Allocated = new int[BankersAlgorithm.NumberOfProcesses, BankersAlgorithm.NumberOfResources];
        for (int i = 0; i < BankersAlgorithm.NumberOfProcesses; i++)
        {
            var line = lines[7 + i].Split(",");
            for (int x = 0; x < BankersAlgorithm.NumberOfResources; x++)
            {
                BankersAlgorithm.Allocated[i, x] = Convert.ToInt32(line[x]);
            }
        }
    }
    public static void GetMaximum(string[] lines)
    {
        BankersAlgorithm.Maximum = new int[BankersAlgorithm.NumberOfProcesses, BankersAlgorithm.NumberOfResources];
        for (int i = 0; i < BankersAlgorithm.NumberOfProcesses; i++)
        {
            var line = lines[9 + BankersAlgorithm.NumberOfProcesses + i].Split(",");
            for (int x = 0; x < BankersAlgorithm.NumberOfResources; x++)
            {
                BankersAlgorithm.Maximum[i, x] = Convert.ToInt32(line[x]);
            }
        }
    }
}