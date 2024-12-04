using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

List<string> read_input (string name)
{
    List<string> strings= new List<string>();
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
    string data = "input.txt";
    List<int> left= new List<int>();
    List<int> right = new List<int>();
    List<string> input = read_input(data);
    for(int i=0; i< input.Count; i++)
    {
        string[] slist = input[i].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        left.Add(int.Parse(slist[0]));
        right.Add(int.Parse(slist[1]));
    
    }
    left.Sort();
    right.Sort();
    for (int i = 0; i < left.Count; i++)
    {
        result += int.Abs(left[i] - right[i]);
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    string data = "input.txt";
    List<int> left = new List<int>();
    List<int> right = new List<int>();
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
    {
        string[] slist = input[i].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        
        left.Add(int.Parse(slist[0]));
        right.Add(int.Parse(slist[1]));
        
    }
    left.Sort();
    right.Sort();
    for(int i=0; i<left.Count; i++)
    {
        int num = left[i];
        List<int> nums = right.FindAll(x => x == num);
        result += num * nums.Count;
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
