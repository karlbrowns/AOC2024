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

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    string concat = "";
    for (int i=0; i<input.Count; i++)
    {
        concat = string.Concat(concat, input[i]);
    }
    string pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";
    Regex r = new Regex(pattern);
    string p2 = "\\d{1,3}";
    Regex r2 = new Regex(p2);
    MatchCollection muls = r.Matches(concat);
    for (int i=0; i<muls.Count; i++)
    {
        string s = muls[i].Value;
        MatchCollection nums = r2.Matches(s);
        int a = int.Parse(nums[0].Value);
        int b = int.Parse(nums[1].Value);
        result += a * b;
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    string concat = "";
    int i = 0;
    for (i = 0; i < input.Count; i++)
    {
        concat = string.Concat(concat, input[i]);
    }
    string pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";
    Regex r = new Regex(pattern);
    string p2 = "\\d{1,3}";
    Regex r2 = new Regex(p2);
    MatchCollection muls = r.Matches(concat);
    string p3 = "do\\(\\)";
    Regex r3 = new Regex(p3);
    string p4 = "don't\\(\\)";
    Regex r4 = new Regex(p4);
    MatchCollection dos = r3.Matches(concat);
    MatchCollection donts = r4.Matches(concat);
    bool enabled = true;
    int pos = 0;
    int dopos = 0;
    int dontpos = 0;
    i = 0;
    bool rest_enabled = false;
    while ((i<muls.Count) && (pos < concat.Length))
    {
        while ((i<muls.Count) && ((rest_enabled) || (muls[i].Index < donts[dontpos].Index)))
        {
            string s = muls[i].Value;
            MatchCollection nums = r2.Matches(s);
            int a = int.Parse(nums[0].Value);
            int b = int.Parse(nums[1].Value);
            result += a * b;
            pos = muls[i].Index + muls[i].Length;
            i++;
        }
        enabled = false;
        while ((!rest_enabled) && (dopos < dos.Count) && (dos[dopos].Index < donts[dontpos].Index)) dopos++;
        if (dopos == dos.Count) break;
        while ((dontpos < donts.Count) && (donts[dontpos].Index < dos[dopos].Index) ) dontpos++;
        while ((i<muls.Count) && (muls[i].Index < dos[dopos].Index)) i++;
        if (dontpos == donts.Count) rest_enabled = true;
        enabled = true;
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
