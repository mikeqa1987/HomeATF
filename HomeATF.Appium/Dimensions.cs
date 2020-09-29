using Castle.Core.Internal;
using HomeATF.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HomeATF.Appium
{
    public class Dimensions : IDimensions
    {
        private int width;
        private int height;

        public Dimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width => this.width;

        public int Height => this.height;
        
    }
}
