using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace IOBot
{
    public class BoxCollection
    {
        Thread boxlocator = new Thread(ScreenCapture.Screenshot);

        public void SearchBox()
        {
            boxlocator.Start();
        }

        public void FindBox()
        {
            boxlocator.Abort();
        }
    }
}
