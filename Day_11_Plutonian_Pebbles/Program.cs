using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Net;

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
    List<Int128> stones = new List<Int128>();
    for (int i=0; i<input.Count; i++)
    {
        string[] nums = input[0].Split(' ',StringSplitOptions.RemoveEmptyEntries);
        for (int j=0; j<nums.Length; j++)
        {
            stones.Add(Int128.Parse(nums[j]));
        }
    }
    for (int i=0; i<25; i++)
    {
        for (int j=0; j<stones.Count; j++)
        {
            if (stones[j] == 0) stones[j] = 1;
            else if ((stones[j].ToString().Length & 1)==0)
            {
                string num = stones[j].ToString();
                string num1 = num.Substring(0, num.Length / 2);
                string num2 = num.Substring(num.Length / 2);
                stones[j] = Int128.Parse(num1);
                stones.Insert(j + 1, Int128.Parse(num2));
                j++;
            }
            else
            {
                stones[j] *= 2024;
            }
        }
        Console.WriteLine(stones.Count);
    }
    Console.WriteLine(stones.Count);
    Console.ReadLine();
}

int find_even_length(Int64 n)
{
    int k;
    k = 0;
    while ((n.ToString().Length & 1) == 1)
    {
        n = 2024 * n;
        k++;

    } 
    return k;

}
int find_steps_to_single(Int64 n)
{
    int k = 0;
    if (n < 10) return k;
    List<Int64> stones = new List<Int64> ();
    stones.Add(n);
    Console.Write(n + " = ");
    while (n>=10)
    {
        int count = stones.Count;
        for (int i = 0; i < count; i++)
        {
            n = stones[i];
            if ((n.ToString().Length & 1) == 0)
            {
                stones[i] = Int64.Parse(n.ToString().Substring(0, n.ToString().Length / 2));
                stones.Add(Int64.Parse(n.ToString().Substring(n.ToString().Length / 2)));
            }
            else stones[i] *= 2024;

        }
        n = stones[0];
        k++;
    }
    Console.Write(stones.Count + ":" + k + ":");
    for (int i = 0; i<stones.Count; i++)
    {
        Console.Write(stones[i] + ",");
    }
    Console.WriteLine();
    return k;
}
//0->1->2024->20,24->2,0,2,4
//2->4048->40,48->4,0,4,8
//2024 = 4:2:2,2,0,4,
//4048 = 4:2:4,4,0,8,
//6072 = 4:2:6,7,0,2,
//8096 = 4:2:8,9,0,6,
//10120 = 8:4:2,2,4,8,0,8,8,0,
//12144 = 8:4:2,9,5,5,4,4,7,6,
//14168 = 8:4:2,6,6,3,8,0,7,2,
//16192 = 7:4:3,2,7,16192,2,6,7,
//18216 = 8:4:3,9,8,8,6,1,6,4,
//54 = 2:1:5,4,
//992917 = 26:8:8,7,60,246,36869184,1,1,60,40,2,6072,102,1,6,2,6,72,581,3,5,72,48,5,592,2,0,
//5270417 = 19:9:8,314,203,395,6,4,497,624,28756992,7,8,424,124,762,7,4,728,384,6,
//2514 = 4:2:2,1,5,4,
//28561 = 8:4:5,7,8,6,7,4,0,4,
//990 = 15:7:8,2,5,5,16192,7,4,6,2,0,3,7,2,4,0,
//246 = 27:9:4,7,402,80,8,6,4,28676032,2,9,716,9,1,9,1,4,578,96,0,6,7,0,5,672,6,6,2,
//102 = 83:13:6,28676032,28676032,24579456,8,809,560,20482880,4,32772608,425,20,6,28676032,20,40,60,2216247616,898,36869184,60,80,80,3473896448,20482880,32772608,40,7,3580407424,2122026368,0,1,28676032,2031901696,60,6,24579456,20,28676032,20482880,10120,2228537344,0,24579456,80,32772608,7,5,3,3,4,8,1,12144,9,1,600,648,1,40,24,2,24,48,72,656,72,96,96,48,4,9,72,8,24,96,3,8,2,0,0,4,6,
//592 = 11:7:4,299,4,16192,275,2,9,17,0,392,4,
//581 = 16:7:4,2,3,7,1,5,2,4,8,1,2,7,7,6,4,4,
Dictionary<(Int64 stone, int steps),Int64> cache = new Dictionary<(Int64, int),Int64>();
Int64 calc_stones(Int64 stone, int steps)
{
    string num = stone.ToString();
    int length = num.Length;
    Int64 count;
    if (steps == 0) return 1;
    if (cache.TryGetValue((stone, steps-1), out count))
    {
        return count;
    }
    else if (stone==0)
    {
        Int64 temp1;
        stone = 1;
        if (cache.TryGetValue((stone, steps - 1), out count))
        {
            temp1 = count;
            return temp1;
        }
        else
        {
            temp1 = calc_stones(stone, steps - 1);
            cache.Add((stone, steps - 1), temp1);
            return temp1;
        }
    }
    //else return calc_stones(stone, steps - 1);
    else if ((length & 1) == 0)
    {
        Int64 stone1 = Int64.Parse(num.Substring(0, length / 2));
        Int64 stone2 = Int64.Parse(num.Substring(length / 2));
        Int64 temp1, temp2;
        if (cache.TryGetValue((stone1, steps -1), out count))
        {
            temp1 = count;
        }
        else
        {
            temp1 = calc_stones(stone1, steps - 1);
            cache.Add((stone1, steps - 1), temp1);
        }
        if (cache.TryGetValue((stone2, steps - 1), out count))
        {
            temp2 = count;
        }
        else
        {
            temp2 = calc_stones(stone2, steps - 1);
            cache.Add((stone2, steps -1), temp2);
        }
        cache.Add((stone, steps - 1), temp1 + temp2);
        return temp1 + temp2;
    }
    else
    {
        Int64 temp1;
            stone *= 2024;
        if (cache.TryGetValue((stone, steps - 1), out count)) {
            temp1 = count;
            return temp1;
        }
        temp1 = calc_stones(stone , steps - 1);
        cache.Add((stone , steps - 1), temp1);
        return temp1;
    }
}
void P2()
{
    Int64 result = 0;
    int index = 0;
    int i = 1;
    String data = "input.txt";
    List<string> input = read_input(data);
    List<Int64> stones = new List<Int64>();
    for (i = 0; i < input.Count; i++)
    {
        string[] nums = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int j = 0; j < nums.Length; j++)
        {
            stones.Add(Int64.Parse(nums[j]));
        }
    }
    for ( i=0 ; i < stones.Count; i++)
    {
        result += calc_stones(stones[i], 26);
        Console.WriteLine(stones[i] + ":" + result);
        
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
