using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper.Functions {

    //drops a command from the app
    public class RemoveFunctionCommand : Command {

        public RemoveFunctionCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        public override void Run() {
            string name = this.Arguments.GetSwitch("-removeFunction");

            //make sure a command name was present
            if (string.IsNullOrEmpty(name)) {
                Console.WriteLine("You need to provide a name of the function to remove.");
                return;
            }
            else if (!this.Settings.HasFunction(name)) {
                Console.WriteLine("No function named '{0}' was found to remove.", name);
                return;
            }

            //removes the command from the app
            Console.WriteLine("Removing command '{0}'", name);
            this.Settings.RemoveFunction(name);
            this.Settings.Save();

        }

    }

}
