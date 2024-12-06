using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

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
    List<List<int>> rules = new List<List<int>>();
    List < List < int >> pages = new List<List< int >> ();
    int updatenum=0;
    List<string> input = read_input(data);
    for (int i=0; i<input.Count; i++)
    {
        if (input[i].Length==0)
        {
            index = 1;
            continue;
        }
        if (index==0)
        {
            string[] nums = input[i].Split('|', StringSplitOptions.RemoveEmptyEntries);
            rules.Add(new List<int>());
            rules[i].Add(int.Parse(nums[0]));
            rules[i].Add(int.Parse(nums[1]));
        }
        if (index==1)
        {
            string[] nums = input[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            pages.Add(new List<int>());
            for (int j=0; j<nums.Length; j++)
            {
                pages[updatenum].Add(int.Parse(nums[j]));
            }
            updatenum++;
        }
    }
    for (int i=0; i<pages.Count; i++)
    {
        bool listbroken = false;
        for (int pos = 0; pos < pages[i].Count; pos++)
        {
            List<List<int>> filtered_before = new List<List<int>>();
            List<List<int>> filtered_after = new List<List<int>>();
            for (int n=0; n<rules.Count; n++)
            {
                if (rules[n][0] == pages[i][pos]) filtered_before.Add(rules[n]);
                if (rules[n][1] == pages[i][pos]) filtered_after.Add(rules[n]);
            }
            for (int p2 = 0; p2 < pages[i].Count; p2++)
            {
                int page = pages[i][p2];
                if (p2 == pos) continue;
                if (p2 < pos)
                {
                    for (int j=0; j<filtered_before.Count; j++)
                    {
                        if (filtered_before[j][1] == page) listbroken = true;
                    }
                }
                if (p2 > pos)
                {
                    for (int j=0; j<filtered_after.Count; j++)
                    {
                        if (filtered_after[j][0] == page) listbroken = true;
                    }
                }
            }
            if (listbroken) break;
        }
        if (!listbroken)
        {
            int pos = pages[i].Count / 2;
            result += pages[i][pos];
        }
    }
    Console.WriteLine(result);
//    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    List<List<int>> rules = new List<List<int>>();
    List<List<int>> pages = new List<List<int>>();
    List<List<int>> brokenpages = new List<List<int>>();
    int updatenum = 0;
    for (int i = 0; i < input.Count; i++)
    {
        if (input[i].Length == 0)
        {
            index = 1;
            continue;
        }
        if (index == 0)
        {
            string[] nums = input[i].Split('|', StringSplitOptions.RemoveEmptyEntries);
            rules.Add(new List<int>());
            rules[i].Add(int.Parse(nums[0]));
            rules[i].Add(int.Parse(nums[1]));
        }
        if (index == 1)
        {
            string[] nums = input[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            pages.Add(new List<int>());
            for (int j = 0; j < nums.Length; j++)
            {
                pages[updatenum].Add(int.Parse(nums[j]));
            }
            updatenum++;
        }
    }
    int p2 = 0, pos;
    for (int i = 0; i < pages.Count; i++)
    {
        bool listbroken = false;
        for (pos = 0; pos < pages[i].Count; pos++)
        {
            List<List<int>> filtered_before = new List<List<int>>();
            List<List<int>> filtered_after = new List<List<int>>();
            for (int n = 0; n < rules.Count; n++)
            {
                if (rules[n][0] == pages[i][pos]) filtered_before.Add(rules[n]);
                if (rules[n][1] == pages[i][pos]) filtered_after.Add(rules[n]);
            }
            for (p2 = 0; p2 < pages[i].Count; p2++)
            {
                int page = pages[i][p2];
                if (p2 == pos) continue;
                if (p2 < pos)
                {
                    for (int j = 0; j < filtered_before.Count; j++)
                    {
                        if (filtered_before[j][1] == page) listbroken = true;
                    }
                }
                if (p2 > pos)
                {
                    for (int j = 0; j < filtered_after.Count; j++)
                    {
                        if (filtered_after[j][0] == page) listbroken = true;
                    }
                }
            }
            if (listbroken) break;
        }
        if (listbroken)
        {
            brokenpages.Add(pages[i]);
        }

    }
    for (int i = 0; i < brokenpages.Count; i++)
    {
        bool listbroken = false;
        for ( pos = 0; pos < brokenpages[i].Count; pos++)
        {
            List<List<int>> filtered_before = new List<List<int>>();
            List<List<int>> filtered_after = new List<List<int>>();
            for (int n = 0; n < rules.Count; n++)
            {
                if (rules[n][0] == brokenpages[i][pos]) filtered_before.Add(rules[n]);
                if (rules[n][1] == brokenpages[i][pos]) filtered_after.Add(rules[n]);
            }
            for (p2 = 0; p2 < brokenpages[i].Count; p2++)
            {
                int page = brokenpages[i][p2];
                if (p2 == pos) continue;
                if (p2 < pos)
                {
                    for (int j = 0; j < filtered_before.Count; j++)
                    {
                        if (filtered_before[j][1] == page) listbroken = true;
                    }
                }
                if (p2 > pos)
                {
                    for (int j = 0; j < filtered_after.Count; j++)
                    {
                        if (filtered_after[j][0] == page) listbroken = true;
                    }
                }
                if (listbroken) break;
            }
            if (listbroken) break;
        }
        if (listbroken)
        {
            if (p2 < pos)
            {
                int temp = brokenpages[i][pos];
                brokenpages[i][pos] = brokenpages[i][p2];
                brokenpages[i][p2] = temp;
            }
            if (p2 > pos)
            {
                int temp = brokenpages[i][p2];
                brokenpages[i][p2] = brokenpages[i][pos];
                brokenpages[i][pos] = temp;
            }
            i--;
        }

    }
    for (int i=0; i<brokenpages.Count; i++)
    {
        pos = brokenpages[i].Count / 2;
        result += brokenpages[i][pos];
    }
    Console.WriteLine(result);
//    Console.ReadLine();
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
 