using System;
using System.IO;

namespace AdditionalCode
{
    class Program
    {
        static void Main(string[] args)
        {
            String input, input1, output;
            //reading
            using (StreamReader sr = new StreamReader(@"D:\input.txt"))
            {
                input = sr.ReadLine();
                input1 = sr.ReadLine();
                sr.Close();
            }
            //converting to additional code
            input = Conversion(input);
            input1 = Conversion(input1);
            //adding
            output = GetSum(input, input1);
            //checking result
            if (!positiveResult(GetNormalNumber(input), GetNormalNumber(input1))) output = Inverse(output);
            //writing
            using (StreamWriter sw = new StreamWriter(@"D:\output.txt"))
            {
                sw.Write(output);
                sw.Close();
            }
        }

        static String Conversion(String input)
        {
            bool minus = input[0] == '-';
            int d = 0;
            if (minus) input = input.Substring(1, input.Length - 1);

            input = GetNormalNumber(input);

            if (minus)
            {
                input = Inverse(input);
                input = GetSum(input, "0000000000000001");
            }

            return input;
        }

        //converting number to 16 bits
        static String GetNormalNumber(String input)
        {
            int d = input[0] == '-' ? 1 : 0;
            if (input.Length > 16 + d) input = input.Substring(0, 16 + d);
            else
            {
                String zeros = "";
                while ((zeros + input).Length < 16 + d)
                {
                    zeros += "0";
                }
                input = input.Insert(d, zeros);
            }
            return input;
        }

        //inverting method
        static String Inverse(String input)
        {
            String output = "";
            for(int i = 0; i < input.Length; i++)
            {
                output += input[i] == '0' ? '1' : '0';
            }
            return output;
        }
        
        //adding method
        static String GetSum(String n1, String n2)
        {
            String output = "";
            bool translation = false;
            for (int i = 15; i >= 0; i--)
            {
                bool a = n1[i] == '1';
                bool b = n2[i] == '1';

                String temp = "0";

                if (translation)
                {
                    translation = a || b;
                    if (a == b) temp = "1";
                }
                else
                {
                    translation = a && b;
                    if(a != b) temp = "1";
                }
                output = output.Insert(0, temp);
            }
            return output;
        }

        //result checking method
        static bool positiveResult(String n1, String n2)
        {
            bool m1 = n1[0] == '-';
            bool m2 = n2[0] == '-';
            if (!(m1 || m2)) return true;
            if (m1 && m2) return false;
            else
            {
                if (m1) n1 = n1.Substring(1, 16);
                if (m2) n2 = n2.Substring(1, 16);

                for(int i = 0; i < 16; i++)
                {
                    if (n1[i] == n2[i]) continue;
                    else if (n1[i] == '1' && !m1) return true;
                    else if (n2[i] == '1' && !m2) return true;
                }
                return false;
            }
        }
    }
}
