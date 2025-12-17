using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Text;
using System.Runtime.Serialization;

namespace Lab56
{
    internal class Program
    {
        static Random randomnum = new Random();
        static int ReadInt(string Message, string ErrorMessage, int min = Int32.MinValue, int max = Int32.MaxValue)
        {
            int number;
            bool isConvert;
            do
            {
                Console.WriteLine(Message);
                isConvert = int.TryParse(Console.ReadLine(), out number);
                if (!isConvert)
                    Console.WriteLine(ErrorMessage);
                else
                {
                    if (min > number || number > max)
                    {
                        Console.WriteLine(ErrorMessage);
                        isConvert = false;
                    }
                }
            } while (!isConvert);
            return number;
        }
        static int[,] CreateRandom2dArray(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = randomnum.Next(-100, 100);
                }
            }
            return matrix;
        }
        static int[,] CreateGiven2dArray(int rows, int columns)
        {
            int element;
            int[,] matrix = new int[columns, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    element = ReadInt($"Введите {j + 1} элемент {i + 1} столбца", "Ошибка ввода целого числа");
                    matrix[i, j] = element;
                }
            }
            return matrix;
        }
        static bool isEmpty2dArray(int[,] matrix)
        {
            return (matrix == null || matrix.Length == 0);
        }
        static void Print2dArray(int[,] matrix)
        {
            if (isEmpty2dArray(matrix))
            {
                Console.WriteLine("Матрица пустая.");
            }
            else
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        Console.Write($"{matrix[i, j],4}");
                    }
                    Console.WriteLine();
                }
            }
        }
        static int[,] Create2dArray()
        {
            PrintMenu("Create2dArray");
            int answer = ReadInt("Выберите способ создания: ", "Ошибка ввода целого числа.", 1, 2);
            int columns = ReadInt("Введите количество столбцов: ", "Ошибка ввода числа", 1);
            int rows = ReadInt("Введите количество строк: ", "Ошибка ввода числа", 1);
            int[,] matrix = new int[rows, columns];
            switch (answer)
            {
                case 1:
                    {
                        matrix = CreateGiven2dArray(rows, columns);
                        Print2dArray(matrix);
                        WorkWith2dArray(matrix, rows, columns);
                        break;
                    }
                case 2:
                    {
                        matrix = CreateRandom2dArray(rows, columns);
                        Print2dArray(matrix);
                        WorkWith2dArray(matrix, rows, columns);
                        break;
                    }
            }
            return matrix;
        }
        static int[,] DellRows(int[,] matrix, int k1, int k2)
        {
            k1--;
            k2--;
            int removeCount = k2 - k1 + 1;
            int[,] tempMatrix = new int[matrix.GetLength(0) - removeCount, matrix.GetLength(1)];

            int ti = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i >= k1 && i <= k2)
                    continue;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    tempMatrix[ti, j] = matrix[i, j];
                }
                ti++;
            }
            return tempMatrix;
        }
        static void WorkWith2dArray(int[,] matrix, int rows, int column)
        {

            int answer;
            do
            {
                if (isEmpty2dArray(matrix))
                {
                    Create2dArray();
                }
                PrintMenu("WorkWith2d");
                answer = ReadInt("Введите номер: ", "Ошибка ввода целого числа.", 1, 3);
                switch (answer)
                {
                    case 1:
                        {
                            int k1 = ReadInt("Введите строку K1", $"Ошибка! Доступный диапазон: 1 - {rows}", 1, rows);
                            int k2 = ReadInt("Введите строку K1", $"Ошибка! Доступный диапазон: {k1} - {rows}", 1, rows);
                            int[,] matrix1 = new int[matrix.GetLength(0) - (k2 - k1), matrix.GetLength(1)];
                            matrix1 = DellRows(matrix, k1, k2);
                            matrix = matrix1;
                            Print2dArray(matrix1);
                            break;
                        }
                    case 2:
                        {
                            Print2dArray(matrix);
                            break;
                        }
                }
            } while (answer != 3);
        }
        static bool IsEmptyjaggedАrray(int[][] jaggedАrray)
        {
            if (jaggedАrray == null || jaggedАrray.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int[][] CreateRandomjaggedАrray(int rows)
        {
            int[][] jaggedАrray = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                int rnd = randomnum.Next(1, 10);
                jaggedАrray[i] = new int[rnd];
                for (int j = 0; j < rnd; j++)
                {
                    jaggedАrray[i][j] = randomnum.Next(-100, 100);
                }
            }
            return jaggedАrray;
        }
        static int[][] CreateGivenjaggedАrray(int rows)
        {
            int[][] jaggedАrray = new int[rows][];
            int element;
            for (int i = 0; i < rows; i++)
            {
                int colomns = ReadInt($"Введите количество столбцов в строке {i + 1}", "Ошибка ввода положительного целого числа", 1);
                jaggedАrray[i] = new int[colomns];
                for (int j = 0; j < colomns; j++)
                {
                    element = ReadInt($"Введите {j + 1} элемент {i + 1} строки", "Ошибка ввода целого числа");
                    jaggedАrray[i][j] = element;
                }
            }
            return jaggedАrray;
        }
        static void PrintjaggedАrray(int[][] jaggedАrray)
        {
            if (IsEmptyjaggedАrray(jaggedАrray))
            {
                Console.WriteLine("Рваный массив пуст");
            }
            else
            {
                for (int i = 0; i < jaggedАrray.GetLength(0); i++)
                {
                    for (int j = 0; j < jaggedАrray[i].GetLength(0); j++)
                    {
                        Console.Write($"{jaggedАrray[i][j],4}");
                    }
                    Console.WriteLine();
                }
            }
        }
        static int[][] CreatejaggedАrray()
        {
            PrintMenu("Create2dArray");
            int answer = ReadInt("Выберите способ создания: ", "Ошибка ввода целого числа.", 1, 2);
            int rows = ReadInt("Введите количество строк: ", "Ошибка ввода числа", 1);
            int[][] jaggedАrray = new int[rows][];
            switch (answer)
            {
                case 1:
                    {
                        jaggedАrray = CreateGivenjaggedАrray(rows);
                        PrintjaggedАrray(jaggedАrray);
                        WorkWithjaggedАrray(jaggedАrray);
                        break;
                    }
                case 2:
                    {
                        jaggedАrray = CreateRandomjaggedАrray(rows);
                        PrintjaggedАrray(jaggedАrray);
                        WorkWithjaggedАrray(jaggedАrray);
                        break;
                    }
            }
            return jaggedАrray;
        }
        static int[][] AddElementTojaggedАrray(int[][] jaggedАrray, int rowNumber, int rowLength)
        {
            int oldRows = jaggedАrray.Length;
            int element;
            int[][] newJaggedАrray = new int[oldRows + 1][];
            rowNumber--;
            for (int i = 0, j = 0; i < newJaggedАrray.Length; i++)
            {
                if (i == rowNumber)
                {
                    newJaggedАrray[i] = new int[rowLength];
                    for (int col = 0; col < rowLength; col++)
                    {
                        element = ReadInt($"Введите {col + 1} элемент {i + 1} строки", "Ошибка ввода целого числа");
                        newJaggedАrray[i][col] = element;
                    }
                }
                else
                {
                    newJaggedАrray[i] = jaggedАrray[j];
                    j++;
                }
            }
            return newJaggedАrray;
        }
        static void WorkWithjaggedАrray(int[][] jaggedАrray)
        {
            int answer;
            do
            {
                PrintMenu("WorkWithJagged");
                answer = ReadInt("Введите номер: ", "Ошибка ввода целого числа.", 1, 3);
                switch (answer)
                {
                    case 1:
                        {
                            int rowNumber = ReadInt("Введите номер добавляемой строки: ", $"Ошибка ввода целого положительного числа. Диапазон доступных значений: 1 - {jaggedАrray.GetLength(0) + 1}", 1, jaggedАrray.GetLength(0) + 1);
                            int rowLength = ReadInt("Введите количество столбцов в новой строке: ", $"Ошибка ввода целого положительного числа.", 1);
                            jaggedАrray = AddElementTojaggedАrray(jaggedАrray, rowNumber, rowLength);
                            break;
                        }
                    case 2:
                        {
                            PrintjaggedАrray(jaggedАrray);
                            break;
                        }
                }
            } while (answer != 3);
        }
        static string ReadString()
        {
            char[] punctuations = { ',', ';', ':', '.', '!', '?' };
            bool isSuccessful = false;
            string givenString;
            do
            {
                isSuccessful = true;
                Console.WriteLine("Введите строку: ");
                givenString = Console.ReadLine();
                if (string.IsNullOrEmpty(givenString))
                {
                    Console.WriteLine("Ошибка! Строка не должна быть пустой.");
                    isSuccessful = false;
                    continue;
                }
                foreach (char sign in punctuations)
                {
                    foreach (char sign2 in punctuations)
                    {
                        string badCombo = ""+sign + sign2;
                        if (givenString.IndexOf(badCombo) != -1)
                        {
                            Console.WriteLine($"Ошибка! В строке не должно быть комбинаций '{badCombo}'");
                            isSuccessful = false;
                            break;
                        }
                    }
                }
            } while (!isSuccessful);

            return givenString;

        }
        static void CreateString()
        {
            PrintMenu("CreateString");
            int answer = ReadInt("Введите номер: ", "Ошибка! Диапазон 1-2.", 1, 2);
            string input;
            if (answer == 1)
            {
                input = ReadString();
                WorkWithString(input);
            }
            else
            {
                input = "В лесу родилась елочка! В лесу она росла. ";
                WorkWithString(input);
            }
        }
        static void WorkWithString(string input)
        {
            int answer;
            do
            {
                PrintMenu("WorkWithString");
                answer = ReadInt("Введите номер: ", "Ошибка ввода целого числа.", 1, 3);
                switch (answer)
                {
                    case 1:
                        {
                            input = ReverseWords(input);
                            Console.WriteLine(input);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine(input);
                            break;
                        }
                }
            } while (answer != 3);
        }
        static string ReverseWords(string givenString)
        {
            char[] punctuations = [',', ':', ';'];
            string result = "";
            MatchCollection sentences = Regex.Matches(givenString, @"[^.!?]+[.!?]");
            foreach (Match match in sentences)
            {
                string resultSentence = "";
                string sentence = match.Value;
                sentence = sentence.Trim();
                char sign = sentence[sentence.Length - 1];
                sentence = sentence.Remove(sentence.Length - 1);
                string[] sentenceArray = sentence.Split(' ');
                for (int i = 0; i < sentenceArray.Length; i++)
                {
                    char[] chars = sentenceArray[i].ToCharArray();
                    Array.Reverse(chars);
                    sentenceArray[i] = new string(chars);
                    sentenceArray[i] = sentenceArray[i].ToLower();
                }

                for (int i = 0; i < sentenceArray.Length; i++)
                {
                    foreach (char punctuation in punctuations)
                    {
                        if (punctuation == sentenceArray[i][0])
                        {
                            sentenceArray[i] = sentenceArray[i].Substring(1) + punctuation;
                        }
                    }

                }
                Array.Sort(sentenceArray);
                foreach (char punctuation in punctuations)//удаление знака препинания перед знаком конца 
                {
                    if(punctuation == sentenceArray[sentenceArray.Length - 1][sentenceArray[sentenceArray.Length - 1].Length - 1])
                    {
                        sentenceArray[sentenceArray.Length - 1] = sentenceArray[sentenceArray.Length - 1].Substring(0, sentenceArray[sentenceArray.Length - 1].Length - 1);
                    }
                }
                foreach (string word in sentenceArray)
                {
                    resultSentence += word + " ";
                }
                resultSentence = resultSentence.Trim();
                resultSentence = char.ToUpper(resultSentence[0]) + resultSentence.Substring(1);
                result += resultSentence + sign + " ";
            }
            return result;
        }
        static void PrintMenu(string type)
        {
            switch (type)
            {
                case "WorkChoice":
                    {
                        Console.WriteLine(@"1. Работа с двумерными массивами
2. Работа с рваными массивами
3. Работа со строками
4. Выход");
                        break;
                    }
                case "Create2dArray":
                    {
                        Console.WriteLine(@"1. Создать массив вручную
2. Создать массив с помощью ДСЧ");
                        break;
                    }
                case "WorkWith2d":
                    {
                        Console.WriteLine(@"1. Удалить строки, начиная со строки К1 и до строки К2 включительно
2. Распечатать массив
3. Назад");
                        break;
                    }
                case "WorkWithJagged":
                    {
                        Console.WriteLine(@"1. Добавить строку с заданным номером
2. Распечатать массив
3. Назад");
                        break;
                    }
                case "CreateString":
                    {
                        Console.WriteLine(@"1. Написать свою строку
2. Взять тестовую строку");
                        break;
                    }
                case "WorkWithString":
                    {
                        Console.WriteLine(@"1. Перевернуть слова в каждом предложении
2. Вывести строку
3. Назад");
                        break;
                    }
            }
        }
        static void Main(string[] args)
        {
            int answer;
            do
            {
                PrintMenu("WorkChoice");
                answer = ReadInt("Выберите функцию: ", "Ошибка, диапазон доступных целых значений 1-4", 1, 4);
                switch (answer)
                {
                    case 1:
                        {
                            Create2dArray();
                            break;
                        }
                    case 2:
                        {
                            CreatejaggedАrray();
                            break;
                        }
                    case 3:
                        {
                            CreateString();
                            break;
                        }
                }
            } while (answer != 4);
        }
    }
}
