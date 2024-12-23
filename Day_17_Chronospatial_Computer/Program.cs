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

/*
 * Op 0 : adv - A = A  / 2^(combo operand)
 * Op 1 : bxl - B = B XOR literal operand
 * Op 2 : bst - B = combo operand % 8
 * Op 3 : jnz - Jump if A NZ - literal operand
 * Op 4 : bxc - B = B XOR C (ignore op)
 * Op 5 : out - Output combo op mod 8, comma separated
 * Op 6 : bdv - B = A / 2^(combo operand)
 * Op 7 : cdv - C = A / 2^(combo operand)
 */
/* operand - literal = 0-7
 * combo   - combo = 0-3 = 0-3, 4 = A, 5 = B, 6 = C, 7 not used
 */

void P1()
{
    int result = 0;
    int index = 0;
    int PC = 0;
    int[] abc = new int[3];
    List<int> p = new List<int>();
    String data = "input.txt";
    char[] seps = { ',', ' ' };
    List<string> input = read_input(data);
    for (int i=0; i<input.Count; i++)
    {
        if (i<3)
        {
            string[] areg = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            abc[i] = int.Parse(areg[2]);
        }
        if (i==4)
        {
            string[] steps = input[i].Split(seps, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in steps)
            {
                if (s[0] == 'P') continue;
                p.Add(int.Parse(s));
            }
        }
    }
    int op = 0;
    while (PC < p.Count)
    {
        switch (p[PC])
        {
            case 0: // adv
                op = p[PC + 1];
                if (op >= 4) op = abc[op - 4];
                abc[0] = abc[0] / (1 << op);
                PC += 2;
                break;
            case 6: // bdv
                op = p[PC + 1];
                if (op >= 4) op = abc[op - 4];
                abc[1] = abc[0] / (1 << op);
                PC += 2;
                break;
            case 7: // cdv
                op = p[PC + 1];
                if (op >= 4) op = abc[op - 4];
                abc[2] = abc[0] / (1 << op);
                PC += 2;
                break;
            case 1: // bxl
                op = p[PC + 1];
                abc[1] = abc[1] ^ op;
                PC += 2;
                break;
            case 2: // bst
                op = p[PC + 1];
                if (op >= 4) op = abc[op - 4];
                abc[1] = op % 8;
                PC += 2;
                break;
            case 3: // jnz
                op = p[PC + 1];
                if (abc[0] != 0)
                {
                    PC = op;
                }
                else PC += 2;
                break;
            case 4: // bxc
                abc[1] = abc[1] ^ abc[2];
                PC += 2;
                break;
            case 5: // out
                op = p[PC + 1];
                if (op >= 4) op = abc[op - 4];
                Console.Write(op % 8 + ",");
                PC += 2;
                break;
        }
    }
    //Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    int PC = 0;
    UInt64[] abc = new UInt64[3];
    List<int> p = new List<int>();
    List<int> p2 = new List<int>();
    String data = "input.txt";
    char[] seps = { ',', ' ' };
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
    {
        if (i < 3)
        {
            string[] areg = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            abc[i] = UInt64.Parse(areg[2]);
        }
        if (i == 4)
        {
            string[] steps = input[i].Split(seps, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in steps)
            {
                if (s[0] == 'P') continue;
                p.Add(int.Parse(s));
            }
        }
    }
    UInt64 op = 0;
    UInt64 A = 0;
    UInt64 B = 0;
    UInt64 C = 0;
    int[] digitsa = new int[2048];
    List<int>[] digits = new List<int>[8];
    for (int i=0; i<8; i++) { digits[i] = new List<int>(); }
    for (A=0; A<2048; A++)
    {
        B = A % 8;
        B ^= 1;
        int b = (int)B;
        C = A >> b;
        B ^= C;
        B ^= 6;
        B %= 8;
        b = (int)B;
        digitsa[(int)A] = b;
        digits[b].Add((int)A);
    }
    // 2,4,1,1,7,5,0,3,4,7,1,6,5,5,3,0
    UInt64[] adds = new UInt64[16];

    while (!Enumerable.SequenceEqual(p, p2))
    {
        p2.Clear();
        //A = 25358015;
        // 2,7,2,5,1,2,7,3,7
        UInt64 A2 = A;
        for (int d16 = 0; d16 < digits[p[15]].Count; d16++)
        {
            UInt64 i16 = (UInt64) digits[p[15]][d16];
            if (i16 > 7) break;
            for (int d15 = 0; d15 < digits[p[14]].Count; d15++)
            {
                UInt64 i15 = (UInt64)digits[p[14]][d15];
                if (i15 > 63) break;
                if ((i15 >> 3) != i16) continue;
                for (int d14 = 0; d14 < digits[p[13]].Count; d14++)
                {
                    UInt64 i14 = (UInt64)digits[p[13]][d14];
                    if (i14 > 511) break;
                    if ((i14 >> 3) != i15) continue;
                    for (int d13 = 0; d13 < digits[p[12]].Count; d13++)
                    {
                        UInt64 i13 = (UInt64)digits[p[12]][d13];
                        if ((i13 >> 3) != (i14 & 255)) continue;
                        for (int d12 = 0; d12 < digits[p[11]].Count; d12++)
                        {
                            UInt64 i12 = (UInt64)digits[p[11]][d12];
                            if (((i12 >> 3) & 2047)!= (i13 & 255)) continue;
                            for (int d11 = 0; d11 < digits[p[10]].Count; d11++)
                            {
                                UInt64 i11 = (UInt64)digits[p[10]][d11];
                                if (((i11 >> 3) & 2047) != (i12 & 255)) continue;
                                for (int d10 = 0; d10 < digits[p[9]].Count; d10++)
                                {
                                    UInt64 i10 = (UInt64)digits[p[9]][d10];
                                    if (((i10 >> 3) & 2047) != (i11 & 255)) continue;
                                    for (int d9 = 0; d9 < digits[p[8]].Count; d9++)
                                    {
                                        UInt64 i9 = (UInt64)digits[p[8]][d9];
                                        if (((i9 >> 3) & 2047) != (i10 & 255)) continue;
                                        for (int d8 = 0; d8 < digits[p[7]].Count; d8++)
                                        {
                                            UInt64 i8 = (UInt64)digits[p[7]][d8];
                                            if (((i8 >> 3) & 2047) != (i9 & 255)) continue;
                                            for (int d7 = 0; d7 < digits[p[6]].Count; d7++)
                                            {
                                                UInt64 i7 = (UInt64)digits[p[6]][d7];
                                                if (((i7 >> 3) & 2047) != (i8 & 255)) continue;
                                                for (int d6 = 0; d6 < digits[p[5]].Count; d6++)
                                                {
                                                    UInt64 i6 = (UInt64)digits[p[5]][d6];
                                                    if (((i6 >> 3) & 2047) != (i7 & 255)) continue;
                                                    for (int d5 = 0; d5 < digits[p[4]].Count; d5++)
                                                    {
                                                        UInt64 i5 = (UInt64)digits[p[4]][d5];
                                                        if (((i5 >> 3) & 2047) != (i6 & 255)) continue;
                                                        for (int d4 = 0; d4 < digits[p[3]].Count; d4++)
                                                        {
                                                            UInt64 i4 = (UInt64)digits[p[3]][d4];
                                                            if (((i4 >> 3) & 2047) != (i5 & 255)) continue;
                                                            for (int d3 = 0; d3 < digits[p[2]].Count; d3++)
                                                            {
                                                                UInt64 i3 = (UInt64)digits[p[2]][d3];
                                                                if (((i3 >> 3) & 2047) != (i4 & 255)) continue;
                                                                for (int d2 = 0; d2 < digits[p[1]].Count; d2++)
                                                                {
                                                                    UInt64 i2 = (UInt64)digits[p[1]][d2];
                                                                    if (((i2 >> 3) & 2047) != (i3 & 255)) continue;
                                                                    for (int d1 = 0; d1 < digits[p[0]].Count; d1++)
                                                                    {
                                                                        UInt64 i1 = (UInt64)digits[p[0]][d1];
                                                                        if (((i1 >> 3) & 2047) != (i2 & 255)) continue;
                                                                        A = (i16 << 45) + ((i15 & 7) << 42) + ((i14 & 7) << 39) + ((i13 & 7) << 36) + ((i12 & 7) << 33) +
                                                                            ((i11 & 7) << 30) + ((i10 & 7) << 27) + ((i9 & 7) << 24) + ((i8 & 7) << 21) +
                                                                            ((i7 & 7) << 18) + ((i6 & 7) << 15) + ((i5 & 7) << 12) + ((i4 & 7) << 9) + ((i3 & 7) << 6) +
                                                                            ((i2 & 7) << 3) + ((i1 & 7));
                                                                        Console.WriteLine(A);
                                                                        p2.Clear();
                                                                        while (A > 0)
                                                                        {
                                                                            B = A % 8;
                                                                            B ^= 1;
                                                                            C = A / (UInt64)(1 << (int)B);
                                                                            A >>= 3;
                                                                            B ^= C;
                                                                            //Console.WriteLine(A + "," + B + "," + C);
                                                                            B ^= 6;
                                                                            B %= 8;
                                                                            p2.Add((int)B);
                                                                        }
                                                                        Console.WriteLine(p2);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        while (A > 0)
        {
            B = A % 8;
            B ^= 1;
            C = A / (UInt64)(1 << (int)B);
            A >>= 3;
            B ^= C;
            //Console.WriteLine(A + "," + B + "," + C);
            B ^= 6;
            B %= 8;
            p2.Add((int)B);
        }
        Console.Write('.');
        if (p2.Count != 16) continue;
        if ((p2[0] == 2) && (p2[1] == 4) && (p2[2] == 1) && (p2[3] == 1)
            && (p2[4] == 7) && (p2[5] == 5) && (p2[6] == 0) && (p2[7] == 3)
            && (p2[8] == 4) && (p2[9] == 7) && (p2[10] == 1) && (p2[11] == 6)
            && (p2[12] == 5) && (p2[13] == 5) && (p2[14] == 3) && (p2[15] == 0))
            Console.WriteLine(A2);

    }
        //p2.Clear();
        //PC = 0;
        //abc[0] = 25358015;
        //if ((abc[0] % 1000) == 0) Console.WriteLine(abc[0]);
        //while (PC < p.Count)
        //{
        //    switch (p[PC])
        //    {
        //        case 0: // adv
        //            op = p[PC + 1];
        //            Console.WriteLine("adv " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            if (op >= 4) op = abc[op - 4];
        //            abc[0] = abc[0] / (1 << (int)op);
        //            PC += 2;
        //            break;
        //        case 6: // bdv
        //            op = p[PC + 1];
        //            Console.WriteLine("bdv " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            if (op >= 4) op = abc[op - 4];
        //            abc[1] = abc[0] / (1 << (int)op);
        //            PC += 2;
        //            break;
        //        case 7: // cdv
        //            op = p[PC + 1];
        //            Console.WriteLine("cdv " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            if (op >= 4) op = abc[op - 4];
        //            abc[2] = abc[0] / (1 << (int)op);
        //            PC += 2;
        //            break;
        //        case 1: // bxl
        //            op = p[PC + 1];
        //            Console.WriteLine("bxl " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            abc[1] = abc[1] ^ op;
        //            PC += 2;
        //            break;
        //        case 2: // bst
        //            op = p[PC + 1];
        //            Console.WriteLine("bst " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            if (op >= 4) op = abc[op - 4];
        //            abc[1] = op % 8;
        //            PC += 2;
        //            break;
        //        case 3: // jnz
        //            op = p[PC + 1];
        //            Console.WriteLine("jnz " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            if (abc[0] != 0)
        //            {
        //                PC = (int)op;
        //            }
        //            else PC += 2;
        //            break;
        //        case 4: // bxc
        //            Console.WriteLine("bxc " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            abc[1] = abc[1] ^ abc[2];
        //            PC += 2;
        //            break;
        //        case 5: // out
        //            op = p[PC + 1];
        //            Console.WriteLine("out " + op + ": " + abc[0] + "," + abc[1] + "," + abc[2]);
        //            if (op >= 4) op = abc[op - 4];
        //            p2.Add((int)(op % 8));
        //            //Console.Write(op % 8 + ",");
        //            PC += 2;
        //            break;
        //    }
        //}
        //A++;
        //Console.ReadLine();
    
    Console.WriteLine(abc[0] - 1);
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
