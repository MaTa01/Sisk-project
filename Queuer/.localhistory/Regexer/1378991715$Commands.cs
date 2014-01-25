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
        public Commands()
        {
            Parse = new RoutedCommand();
        }
    }
}
