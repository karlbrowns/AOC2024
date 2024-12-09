using System.Text.RegularExpressions;
using System.Diagnostics;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

Int64 evaluate(List<int> operands, Int64 operators)
{
    Int64 result = operands[0];
    int index = 1;
    while (operators != 1) 
    {
        switch (operators & 1)
        {
            case 0: result += operands[index]; break;
            case 1: result *= operands[index]; break;
        }
        operators >>= 1;
        index++;
    }
    return result;
}
Int128 evaluate2(List<int> operands, Int128 operators)
{
    Int128 result = operands[0];
    int index = 1;
    while (operators != 1)
    {
        // nasty fix to save me trying to count in 3s for each bit pair.
        if ((int)(operators & 3)==3)
        {
            result = 0;
            break;

        }
        switch ((int)(operators & 3))
        {
            case 0: result += operands[index]; break;
            case 1: result *= operands[index]; break;
            case 2:
                {
                    string s1 = result.ToString();
                    string s2 = operands[index].ToString();
                    s1 = s1 + s2;
                    result = Int128.Parse(s1);
                }
                break;
            
        }
        operators >>= 2;
        index++;
    }
    return result;
}
void P1()
{
    Int64 result = 0;
    int index = 0;
    String data = "input.txt";
    List<Int64> values= new List<Int64>();
    List<List<int>> operands = new List<List<int>>();
    char[] sep = new char[] { ' ', ':' };
    List<string> input = read_input(data);
    for (int i=0; i<input.Count; i++)
    {
        string[] nums = input[i].Split(sep, StringSplitOptions.RemoveEmptyEntries);
        values.Add(Int64.Parse(nums[0]));
        operands.Add(new List<int>());
        for (int j=1; j<nums.Length; j++)
        {
            operands[i].Add(int.Parse(nums[j]));
        }
    }
    for (int i=0; i<values.Count; i++)
    {
        int ops = operands[i].Count - 1;
        Int64 operators = 1<<ops;
        for ( ; operators<(1<<(ops+1)) ; operators++)
        {
            Int64 test = evaluate(operands[i], operators);
            if (test == values[i])
            {
                result += values[i];
                break;
            }

        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}
//189207837752840 is too high
//189207837752840
//189207837752840
//189207836795655
void P2()
{
    Int128 result = 0;
    int index = 0;
    String data = "input.txt";
    List<Int128> values = new List<Int128>();
    List<List<int>> operands = new List<List<int>>();
    char[] sep = new char[] { ' ', ':' };
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
    {
        string[] nums = input[i].Split(sep, StringSplitOptions.RemoveEmptyEntries);
        values.Add(Int128.Parse(nums[0]));
        operands.Add(new List<int>());
        for (int j = 1; j < nums.Length; j++)
        {
            operands[i].Add(int.Parse(nums[j]));
        }
    }
    for (int i = 0; i < values.Count; i++)
    {
        if (values[i]==62)
        {
            Console.WriteLine(62);
        }
        int ops = operands[i].Count - 1;
        Int128 operators = 1 << (2*ops);
        for (; operators < (1 << (2*ops + 1)); )
        {
            Int128 test = evaluate2(operands[i], operators);
            if (test == values[i])
            {
                result += values[i];
                Console.Write(values[i] + " = ");
                for(int j=0; j < operands[i].Count - 1; j++)
                {
                    Console.Write(operands[i][j]);
                    int temp = (int)((operators >> (2 * j)) & 3);
                    Console.Write((temp == 2) ? "||" : (temp == 1) ? "*" : "+");
                }
                Console.WriteLine(operands[i][operands[i].Count - 1]);
                break;
            }
            operators = ((operators & 3) < 2) ? operators + 1 : operators + 2;

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
