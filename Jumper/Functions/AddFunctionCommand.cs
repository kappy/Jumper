using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jumper.Functions;

namespace Jumper.Functions {

    public class AddFunctionCommand : Command {

        public AddFunctionCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        //adds a new command 
        public override void Run() {
            string name = this.Arguments.GetSwitch("-addFunction");
            string alias = this.Arguments.GetSwitch("-alias");

            //make sure the function name was present
            if (string.IsNullOrEmpty(name)) {
                Console.WriteLine("You need to provide the name of the function to add.");
                return;
            }
            if (string.IsNullOrEmpty(alias)) {
                alias = name;
            }
                        
            if (string.IsNullOrEmpty(this.Arguments.Command)) {
                Console.WriteLine("You need to provide the full qualified type name for the class.");
                return;
            }

            Function function = new Function {
                Alias = alias,
                Name = name,
                Class = this.Arguments.Command
            };

            //check of this replaces or not
            if (this.Settings.HasFunction(alias)) Console.WriteLine("Replacing function '{0}'", alias);
            else Console.WriteLine("Added function '{0}'", alias);
            this.Settings.AddFunction(function);
            this.Settings.Save();
        }

    }

}
