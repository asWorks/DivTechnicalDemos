using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFoldersFilesSort
{
    public class AnonymusTest
    {
        // Function dei drei string Parameter sowie einen vordefinierten        //System Delegaten mit drei String Parameter und bool als Rückgabewert erwaetet.
        public static string ShowNames(string[] TheNames, string comparer, string Comment, Func<string, string, string, bool> TheFunction)
        {
            foreach (var s in TheNames)
            {
                if (TheFunction(s, comparer, Comment))
                {
                    Console.WriteLine(Comment);
                    Console.WriteLine(s);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Comment);
                    sb.AppendLine();
                    sb.AppendLine(s);
                    return sb.ToString();
                }
            }

            return "false";

        }

        public static string ShowNamesNew<T>(string[] TheNames, string comparer, string Comment, Func<string[],string, string, string> TheFunction)
        {
            return TheFunction(TheNames, comparer, Comment);

        }



        public static T1 CalcGenT1<T, T1>(T p1, T p2, Func<T, T, T1> Result)
        {
            return Result(p1, p2);
        }

    }
}
