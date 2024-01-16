using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bottle_gAME_ST
{
    public class Colors
    {
        private string color;
        public Colors() { color = ""; }
        public Colors(string color) { this.color = color; }
        public void SetColor(string color) { this.color = color; }
        public string GetColor() { return color; }
        public void PrintColor()
        {
            Console.WriteLine(color);
        }

    }
}
