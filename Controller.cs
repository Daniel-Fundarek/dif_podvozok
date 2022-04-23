using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace dif_podvozok
{
    internal class Controller
    {
        private double L = 0.2;  // m  rozchod kolies
        private double d = 0.1;  // m  vzdialenost medzi taziskom a kolesom
        private double r = 0.05; // m  polomer kolesa
        // zadane premenne
        private double[] velLeftWheelArray = new double[]{ 2, -1, 0, 2, 1 };
        private double[] velRightWheelArray = new double[] {2, 1, 0, -2, 1};
        private double velRightWheel = 0;
        private double velLeftWheel = 0;
        private double[] timeStamps = new double[]{0, 5, 10, 15, 20};
        private int timeIndex = 0;
        private double timeDev = 0.1; //s
        private Timer timer;
        private double time = 0; //s

        private AutoResetEvent autoReset = new AutoResetEvent(true);
        // premenne ktore pocitam
        private double rotVel; // rad/s
        private double linVel; // m/s
        private double angle;  // rad
        private double currTime; //s
        private Position prevPosition = new Position();
        private Position position = new Position();
        private Canvas myCanvas;

        public Controller(Canvas myCanvas)
        {

            this.myCanvas = myCanvas;
            
            timer = new Timer(timerCallback,autoReset , 0, (int)(timeDev * 1000));



        }

        private double calculateLinVel(double leftWheelVel, double RightWheelVel)
        {
            return (leftWheelVel + RightWheelVel) / 2;
        }

        private double calculateRotVel(double leftWheelVel, double rightWheelVel, double wheelBase)
        {
            return (rightWheelVel - leftWheelVel) / wheelBase;
        }

        private double calculateAngle(double rotVelocity, double previousAngle, double timeDeviation)
        {
            return previousAngle + rotVelocity * timeDeviation;
        }

        private void calculatePosition(Position _position,double _linVel, double angleR, double timeDeviation)
        {
            double linDeviation =_linVel * timeDeviation;
            _position.setX(_position.getX() + linDeviation * Math.Sin(angleR));
            _position.setY(_position.getY() + linDeviation * Math.Cos(angleR));

        }

        private void changeVelOfWheel()
        {
            if (timeStamps[timeIndex] <= currTime)
            {
                velLeftWheel = velLeftWheelArray[timeIndex];
                velRightWheel = velRightWheelArray[timeIndex];
                timeIndex++;
                
            }
        }
        private void timerCallback(Object stateInfo)
        {
           
            changeVelOfWheel();
            linVel = calculateLinVel(velLeftWheel, velRightWheel);
            rotVel = calculateRotVel(velLeftWheel, velRightWheel, L);
            angle = calculateAngle(rotVel, angle, timeDev);
            prevPosition = position;
            calculatePosition(position,linVel,angle,timeDev);
            drawLine(myCanvas,prevPosition,position);
            
        }

        public void drawLine(Canvas MyCanvas, Position lastPosition, Position currPosition)
        {
           // MyCanvas.ActualHeight;
         //   MyCanvas.ActualWidth;
            Line line = new Line();
            line.X1 = lastPosition.IntX + MyCanvas.ActualWidth;
            line.Y1 = lastPosition.IntY + MyCanvas.ActualHeight;
            line.X2 = currPosition.IntX + MyCanvas.ActualWidth;
            line.Y2 = currPosition.IntY + MyCanvas.ActualHeight;
            line.Fill = Brushes.Black;
            line.Stroke = Brushes.Aqua;
            MyCanvas.Children.Add(line);
        }
        
    }

}
