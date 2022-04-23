﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dif_podvozok
{
    internal class Position
    {
        private double x;
        private double y;

        public int IntX { get; set; }
        public int IntY { get; set; }

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Position()
        {
            this.x = 0;
            this.y = 0;
        }


        public void setX(double x)
        {
            this.x = x;
            this.IntX = (int) x;
        }

        public void setY(double y)
        {
            this.y = y;
            this.IntY = (int) y;
        }

        public double getX()
        {
            return this.x;
        }
        public double getY()
        {
            return this.y;
        }
    }
}
