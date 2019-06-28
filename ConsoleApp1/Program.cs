using System;

namespace ConsoleAppEngine
{
    public class BrainV
    {
        static char[,] arr = new char[7, 7];
        static int[] inir = new int[33];
        static int[] finr = new int[33];
        static int[] inic = new int[33];
        static int[] finc = new int[33];
        static int flag = 0, s = 0;

        public static void Copy(char[,] ar1, char[,] ar2)
        {
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    ar2[i, j] = ar1[i, j];
        }

        public static int Check1(char[,] chk)
        {
            int c = 0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    if (chk[i, j] == 'o')
                        c++;
            return c;
        }

        public static int Check2(int[,] num)
        {
            int f = 0;
            for (int i = 0; i < 7; i++)
            {

                for (int j = 0; j < 7; j++)
                {
                    if ((num[j, j] == ' ' && num[i + 1, j] == 'o' && num[i + 2, j] == 'o') || (num[j, j] == ' ' && num[i - 1, j] == 'o' && num[i - 2, j] == 'o') || (num[j, j] == ' ' && num[i, j + 1] == 'o' && num[i, j + 2] == 'o') || (num[j, j] == ' ' && num[i, j - 1] == 'o' && num[i, j - 2] == 'o'))
                    {
                        f = 1;
                        break;
                    }
                }
                if (f == 1)
                    break;
            }
            return f;
        }

        public static void PrintGrid(char[,] arr)
        {
            Console.WriteLine("Column No: 1234567");
            Console.WriteLine("Row1 ->    " + arr[0, 0] + arr[0, 1] + arr[0, 2] + arr[0, 3] + arr[0, 4] + arr[0, 5] + arr[0, 6]);
            Console.WriteLine("Row2 ->    " + arr[1, 0] + arr[1, 1] + arr[1, 2] + arr[1, 3] + arr[1, 4] + arr[1, 5] + arr[1, 6]);
            Console.WriteLine("Row3 ->    " + arr[2, 0] + arr[2, 1] + arr[2, 2] + arr[2, 3] + arr[2, 4] + arr[2, 5] + arr[2, 6]);
            Console.WriteLine("Row4 ->    " + arr[3, 0] + arr[3, 1] + arr[3, 2] + arr[3, 3] + arr[3, 4] + arr[3, 5] + arr[3, 6]);
            Console.WriteLine("Row5 ->    " + arr[4, 0] + arr[4, 1] + arr[4, 2] + arr[4, 3] + arr[4, 4] + arr[4, 5] + arr[4, 6]);
            Console.WriteLine("Row6 ->    " + arr[5, 0] + arr[5, 1] + arr[5, 2] + arr[5, 3] + arr[5, 4] + arr[5, 5] + arr[5, 6]);
            Console.WriteLine("Row7 ->    " + arr[6, 0] + arr[6, 1] + arr[6, 2] + arr[6, 3] + arr[6, 4] + arr[6, 5] + arr[6, 6]);
        }

        public static void Compute(char[,] num)
        {
            if (Check1(num) == 1)
                flag = 1;
            char[,] temp = new char[7, 7];
            Copy(num, temp);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //if(arr[i, j]==0 && (arr[i-1, j]==1 || arr[i+1, j]==1 || arr[i, j+1]==1 || arr[i, j-1]==1))
                    if ((i + 2) < 7)
                    {
                        //Console.WriteLine(num[i, j]+" "+num[i+1, j]+" "+num[i+2, j]);

                        if (num[i, j] == ' ' && num[i + 1, j] == 'o' && num[i + 2, j] == 'o')
                        {
                            temp[i + 1, j] = temp[i + 2, j] = ' ';
                            temp[i, j] = 'o';
                            Compute(temp);
                            if (flag == 1)
                            {
                                inir[s++] = i + 2;
                                inic[s] = j;
                                finr[s] = i;
                                finc[s] = j;
                            }
                        }
                    }
                    if ((i - 2) >= 0)
                    {
                        if (num[i, j] == ' ' && num[i - 1, j] == 'o' && num[i - 2, j] == 'o')
                        {
                            temp[i - 1, j] = temp[i - 2, j] = ' ';
                            temp[i, j] = 'o';
                            Compute(temp);
                            if (flag == 1)
                            {
                                inir[s++] = i - 2;
                                inic[s] = j;
                                finr[s] = i;
                                finc[s] = j;
                            }
                        }
                    }
                    if ((j + 2) < 7)
                    {
                        if (num[i, j] == ' ' && num[i, j + 1] == 'o' && num[i, j + 2] == 'o')
                        {
                            temp[i, j + 1] = temp[i, j + 2] = ' ';
                            temp[i, j] = 'o';
                            Compute(temp);
                            if (flag == 1)
                            {
                                inir[s++] = i;
                                inic[s] = j + 2;
                                finr[s] = i;
                                finc[s] = j;
                            }
                        }
                    }
                    if ((j - 2) > 0)
                    {
                        if (num[i, j] == ' ' && num[i, j - 1] == 'o' && num[i, j - 2] == 'o')
                        {
                            temp[i, j - 1] = temp[i, j - 2] = ' ';
                            temp[i, j] = 'o';
                            Compute(temp);
                            if (flag == 1)
                            {
                                inir[s++] = i;
                                inic[s] = j - 2;
                                finr[s] = i;
                                finc[s] = j;
                            }
                        }
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            int r, c;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    if ((i == 2 || i == 3 || i == 4) || (j == 2 || j == 3 || j == 4))
                        arr[i, j] = 'o';
                    else
                        arr[i, j] = '*';

            Console.WriteLine("Column No: 1 2 3 4 5 6 7");
            Console.WriteLine("Row1 ->    0|0|1|1|1|0|0");
            Console.WriteLine("Row2 ->    0|0|1|1|1|0|0");
            Console.WriteLine("Row3 ->    1|1|1|1|1|1|1");
            Console.WriteLine("Row4 ->    1|1|1|1|1|1|1");
            Console.WriteLine("Row5 ->    1|1|1|1|1|1|1");
            Console.WriteLine("Row6 ->    0|0|1|1|1|0|0");
            Console.WriteLine("Row7 ->    0|0|1|1|1|0|0");
            Console.WriteLine("\nEnter the row and column no. to place the hole:");
            r = int.Parse(Console.ReadLine());
            c = int.Parse(Console.ReadLine());
            arr[r - 1, c - 1] = '\0';
            Console.WriteLine("The new modified grid is:");
            PrintGrid(arr);


            Console.WriteLine("*" + arr[3, 3] + "*");
            Console.WriteLine("*" + arr[4, 3] + "*");
            Console.WriteLine("*" + arr[5, 3] + "*");
            if (arr[3, 3] == ' ' && arr[4, 3] == 'o' && arr[5, 3] == 'o')
                Console.WriteLine(arr[3, 3] + " " + arr[3 + 1, 3] + " " + arr[3 + 2, 3]);


            //Compute(arr);
            Console.WriteLine(s);
            Console.WriteLine("Hello");
        }
    }
}