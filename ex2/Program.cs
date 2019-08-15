using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    public class Program
    {
        static int length = 5;

        static void Main(string[] args)
        {
            int counter = 0;
            string line;
            string input;
            List<string> result = new List<string>();

            while (true)
            {
                System.IO.StreamReader file =
                    new System.IO.StreamReader(Directory.GetCurrentDirectory() + "/data.txt");

                Console.WriteLine("Please Insert 5 letters"); 
                input = Console.ReadLine();
                Console.WriteLine("input = " + input);
                while ((line = file.ReadLine()) != null)
                {
                    if (IsInputEqualLine(line, input))
                    {
                        result.Add(line);
                    }
                    counter++;
                }
                Console.WriteLine();
                Console.WriteLine("Find " + result.Count + " Results");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

                foreach (string i in result)
                {
                    Console.Write("{0}\t", i.ToString());
                }
                Console.WriteLine("\n\n");
                file.Close();
                result.Clear();
            }
        }

        public static bool IsInputEqualLine(string line, string input)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (line[i] == input[j])
                    {
                        input = input.Remove(j, 1);
                        input = input.Insert(j, "*");
                        line = line.Remove(i, 1);
                        line = line.Insert(i, "*");
                    }
                }
            }

            if (line.Equals(input))
                return true;

            return false;
        }
    }
}
