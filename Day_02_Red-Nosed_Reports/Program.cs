using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    List<List<int>> levels = new List<List<int>>();
    for (int i=0; i<input.Count; i++)
    {
        string[] nums = input[i].Split(' ',StringSplitOptions.RemoveEmptyEntries);
        levels.Add(new List<int>());
        for (int j = 0; j<nums.Length ; j++)
        {
            levels[i].Add(int.Parse(nums[j]));
        }
    }
    for (int i=0; i<levels.Count; i++)
    {
        if (levels[i].Count > 1)
        {
            bool incr = (levels[i][0] < levels[i][1]);
            int j = 1;
            while (j < levels[i].Count)
            {
                int diff = levels[i][j] - levels[i][j - 1];
                if ((int.Abs(diff) <= 3) && (diff != 0) && (incr?diff>0:diff<0)) j++;
                else break;
            }
            if (j == levels[i].Count) result++;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

int test_data (List<int> levels)
{
    if (levels.Count > 1)
    {
        bool incr = (levels[0] < levels[1]);
        int j = 1;
        while (j < levels.Count)
        {
            int diff = levels[j] - levels[j - 1];
            if ((int.Abs(diff) <= 3) && (diff != 0) && (incr ? diff > 0 : diff < 0)) j++;
            else break;
        }
        if (j == levels.Count) return 1;
        else return 0;

    }
    else return 1;

}
void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    List<List<int>> levels = new List<List<int>>();
    for (int i = 0; i < input.Count; i++)
    {
        string[] nums = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        levels.Add(new List<int>());
        for (int j = 0; j < nums.Length; j++)
        {
            levels[i].Add(int.Parse(nums[j]));
        }
    }
    for (int i = 0; i < levels.Count; i++)
    {
        if (levels[i].Count > 1)
        {
            bool incr = (levels[i][0] < levels[i][1]);
            int j = 1;
            int probs = 0;
            while (j < levels[i].Count)
            {
                int diff = levels[i][j] - levels[i][j - 1];
                if (((int.Abs(diff) <= 3) && (diff != 0) && (incr ? diff > 0 : diff < 0)))
                {
                    j++;
                }
                else break;
            }
            if (j == levels[i].Count) result++;
            else
            {
                for (j = 0; j < levels[i].Count; j++)
                {
                    List<int> sublevels = new List<int>();
                    for (int k = 0; k< levels[i].Count; k++)
                    {
                        if (k != j) sublevels.Add(levels[i][k]);

                    }
                    int res = test_data(sublevels);
                    if (res==1)
                    {
                        result++;
                        break;
                    }
                }
            }
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
