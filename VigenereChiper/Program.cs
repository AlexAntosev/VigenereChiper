using System;

namespace VigenereChiper
{
    class Program
    {
        static readonly char[] alphabet = MakeAlphabet();
        static readonly char[,] matrix = GenerateMatrix();

        static void Main(string[] args)
        {
            string input = "";
            while (input != "0")
            {
                Console.WriteLine("Enter \"1\" if you want to encrypt text");
                Console.WriteLine("Enter \"2\" if you want to decrypt text");
                Console.WriteLine("Enter \"0\" if you want to exit");
                Console.Write("Your choice: ");
                input = Console.ReadLine();
                ShowMenu(input);
            } 
        }

        public static void ShowMenu(string input)
        {            
            switch (input)
            {
                case "1":
                    Encrypt();
                    break;
                case "2":
                    Decrypt();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("You entered invalid value. Please repeat... ");
                    break;
            }
            Console.WriteLine();
        }

        public static void Encrypt()
        {
            Console.Write("Enter the message you want to encrypt: ");
            string message = Console.ReadLine().ToLower();
            Console.Write("Enter a key: ");
            string key = Console.ReadLine();
            string extendedKey = MakeExtendedKey(message, key);
            Console.Write("New word : ");
            for (int i = 0; i < extendedKey.Length; i++)
            {
                if (Array.Exists(alphabet,x => x == message[i]))
                {
                    int rowIndex = Array.IndexOf(alphabet, message[i]);
                    int colIndex = Array.IndexOf(alphabet, extendedKey[i]);
                    char newWordSymbol = matrix[rowIndex, colIndex];
                    Console.Write(newWordSymbol);                  
                }
                else Console.Write(message[i]);
            }
        }

        public static void Decrypt()
        {
            Console.Write("Enter the message you want to decrypt: ");
            string message = Console.ReadLine().ToLower();
            Console.Write("Enter a key: ");
            string key = Console.ReadLine();
            string extendedKey = MakeExtendedKey(message, key);
            Console.Write("New word : ");            
            for (int i = 0; i < extendedKey.Length; i++)
            {
                if (Array.Exists(alphabet, x => x == message[i]))
                {
                    int rowIndex = Array.IndexOf(alphabet, extendedKey[i]);
                    int colIndex = 0;
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (matrix[rowIndex, j] == message[i])
                        {
                            colIndex = j;
                        }
                    }
                    Console.Write(matrix[0, colIndex]);
                }
                else Console.Write(message[i]);                
            }
        }

        public static string MakeExtendedKey(string message, string key)
        {
            string resultKey = "";
            for (int i = 0; i < message.Length; i++)
            {
                int index = i % key.Length;
                char symbol = key[index];
                resultKey += symbol;
            }
            return resultKey;
        }

        public static char[] MakeAlphabet()
        {
            char[] alphabet = new char[26];
            char symbol = 'a';
            for (int i = 0; i < 26; i++)
            {
                alphabet[i] = symbol;
                symbol++;
            }
            return alphabet;
        }             

        public static char[,] GenerateMatrix()
        {
            char[,] matrix = new char[27, 27];
            for (int i = 0; i < alphabet.Length + 1; i++)
            {
                int symbolIndex = 0;
                bool account = true;
                for (int j = 0; j < alphabet.Length + 1; j++)
                {
                    if (account)
                    {
                        symbolIndex = i + j;
                    }
                    if (symbolIndex > 25)
                    {
                        symbolIndex = 0;
                        account = false;
                    }
                    matrix[i, j] = alphabet[symbolIndex];
                    symbolIndex++;
                }
            }
            return matrix;
        }        
    }
}
