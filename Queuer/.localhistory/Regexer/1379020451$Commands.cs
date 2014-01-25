using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Regexer
{
    static class Commands
    {
        public static RoutedCommand Parse { get; set; }
        static Commands()
        {
            Parse = new RoutedCommand();
            BindCommands();
        }

        private static void BindCommands()
        {
            Parse.InputGestures.Add( new KeyGesture( Key.S , ModifierKeys.Control ));
        }

    }
}
