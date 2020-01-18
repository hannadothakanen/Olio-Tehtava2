using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtävä2
{
    class JobChangedEventArgs: System.EventArgs
    {
        public Job Job { get; set; }
    }
}
