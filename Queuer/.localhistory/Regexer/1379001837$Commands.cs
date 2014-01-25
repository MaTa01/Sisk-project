using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Regexer
{
    static class Initialise
    {
        public static RoutedCommand Parse { get; set; }
        public static Initialise()
        {
            Parse = new RoutedCommand();
            BindCommands();
        }

        private void BindCommands()
        {
            Parse.InputGestures.Add( new KeyGesture( Key.S , ModifierKeys.Control ));
        }

    }
}
