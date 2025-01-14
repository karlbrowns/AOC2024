using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}
string[] towels;
Dictionary<(string, int),bool> cache = new Dictionary<(string, int),bool>();
bool test(string t)
{
    bool result = false;

    if (t.Length == 0) return true;
    for (int j = 0; j < towels.Length; j++)
    {
        if (cache.TryGetValue((t, j), out result) == false)
        {
            if (t.StartsWith(towels[j]))
            {
                result = test(t.Substring(towels[j].Length));
                cache.Add((t, j), result);
            }
        }
        if (result) return true;
    }

    return result;
}
void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    towels = input[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);
    Array.Sort(towels);
    for (int i=2; i<input.Count; i++)
    {
        if (test(input[i])) result++;
        Console.Write(i-2 + ",");
    }
    
    Console.WriteLine(result);
    Console.ReadLine();
}

Dictionary<(string, int), bool> cache2 = new Dictionary<(string, int), bool>();
Dictionary<(string, int), Int64> cache3 = new Dictionary<(string, int), Int64>();

bool test2(string t, ref Int64 total)
{
    bool r = false;
    bool result = false;
    Int64 t2 = 0;
    ref Int64 total2 = ref t2;

    if (t.Length == 0)
    {
        total++;
        return true;
    }
    for (int j = 0; j < towels.Length; j++)
    {
        if (cache2.TryGetValue((t, j), out result) == false)
        {
            if (t.StartsWith(towels[j]))
            {
                result = test2(t.Substring(towels[j].Length), ref total2);
                cache2.Add((t, j), result);
                cache3.Add((t, j), total2);
            }
            if (total2 > 0)
            {
                total += total2;
                total2 = 0;
            }
        }
        else
        {
            total += cache3[(t,j)];
        }
        
        //if (result) return true;
    }

    return result;
}
void P2()
{
    Int64 res = 0;
    Int64 r2 = 0;
    ref Int64 result = ref res;
    bool b = false;
    ref bool b2 = ref b;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    towels = input[0].Split(", ", StringSplitOptions.RemoveEmptyEntries);
    Array.Sort(towels);
    cache.Clear();
    for (int i = 2; i < input.Count; i++)
    {
        test2(input[i], ref result);
        for (int j=0; j<towels.Length; j++)
        {
            if (cache3.TryGetValue((input[i],j), out res))
            {
                r2 += cache3[(input[i], j)];
            }
        }
        Console.Write(i - 2 + ",");
    }
    Console.WriteLine(r2);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
//P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
