using System;
using System.Collections.Generic;
using System.IO;

namespace TestTask
{
    class Program
    {
        private static int _n;
        private static int[][] _arr;
        private static bool _sorted = false;
        static void Main()
        {
            
            while (_n <= 0)
            {
                Console.WriteLine("Введите целое число N:");         
                string text = Console.ReadLine();

                if (int.TryParse(text, out _n))
                {
                    if (_n <= 0)
                    {
                        Console.WriteLine("Вы ввели не положительное число(");
                    }
                    else
                    {
                        Console.WriteLine("Супер, Вы ввели целое число!");
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели нецелое, повторите ввод...");
                }
            }
            CreateArray();
            //PrintToFile();// вывод не отсортированного массива в C:\TestTaskMarinAleksei
            SortArray(); 
            //PrintToFile();// вывод отсортированного массива в C:\TestTaskMarinAleksei
            PrintArray(); // вывод отсортированного массива в консоль
        }

        static void SortArray()
        {
            for (int i=0; i < _n; i++)
            {
                _arr[i] = Merge(_arr[i]);
                if (i % 2 == 1)
                {
                    Array.Reverse(_arr[i]);
                }
            }
            _sorted = true;
        }

        static void PrintArray()
        {
            for (int i = 0; i < _n; i++)
            {
                Console.WriteLine("Array - "+i);

                for (int j = 0; j < _arr[i].Length; j++)
                {
                    Console.Write(_arr[i][j]+" ");
                }

                Console.WriteLine();
            }
        }

        static void CreateArray()
        {
            var rand = new Random();
            var knownNumbers = new HashSet<int>();
            _arr = new int[_n][];

            for (int i = 0; i < _n; i++)
            {
                int newElement;
                do
                {
                    newElement = rand.Next(1,_n*2);
                } while (!knownNumbers.Add(newElement));

                _arr[i] = new int[newElement];

                for(int j = 0; j < newElement; j++)
                {
                    _arr[i][j] = rand.Next();
                }
            }  
        }


        static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        //сортировка слиянием
        static int[] Merge(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                Merge(array, lowIndex, middleIndex);
                Merge(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }

        static int[] Merge(int[] array)
        {
            return Merge(array, 0, array.Length - 1);
        }
        
        
        
        public static void PrintToFile()
        {
            string path = "C:\\TestTaskMarinAleksei";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            try
            {
                StreamWriter sw;
                if (_sorted)
                {
                    sw = new StreamWriter("C:\\TestTaskMarinAleksei\\SortedArrays.csv");
                }
                else
                {
                    sw = new StreamWriter("C:\\TestTaskMarinAleksei\\NotSortedArrays.csv");
                }
                for(int i = 0; i < _n; i++)
                {
                    sw.Write("Array - "+i+";");
                    for(int j = 0; j < _arr[i].Length; j++)
                    {
                        sw.Write(_arr[i][j] + ";");
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Successfull output!");
            }
        }
    }
}


