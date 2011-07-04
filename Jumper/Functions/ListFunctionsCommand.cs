using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper.Functions {

    public class ListFunctionsCommand : Command {

        public ListFunctionsCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        public override void Run() {
            if (this.Settings.Functions.Count == 0) {
                Console.WriteLine("No functions are registered yet.");
                return;
            }

            //get the longest command name
            int longestAlaias = this.Settings.Functions.Max(item => item.Alias.Length) + 5;
            int longest = this.Settings.Functions.Max(item => item.Name.Length) + 5;

            //add each item
            Console.Write("Alaias".PadRight(longestAlaias));
            Console.Write("Name".PadRight(longest));
            Console.WriteLine("Class");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - -");

            //show each item
            foreach (var function in this.Settings.Functions) {
                Console.Write(function.Alias.PadRight(longestAlaias));
                Console.Write(function.Name.PadRight(longest));
                Console.WriteLine(function.Class);
            }

            Console.WriteLine();

        }

    }

}
