using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApplication3
{
    public delegate void EventHandalerX(TrafficSignal obj);
 
    class Program
    {
        public static event EventHandalerX InitiateEvent;
        static void Main(string[] args)
        {
            TrafficSignal objTrafficSignal = new TrafficSignal();
            InitiateEvent += new EventHandalerX(TrafficSignal.Invokemethod);
            InitiateEvent.Invoke(objTrafficSignal);
        }
    }

    /// <summary>
    /// TRaffic Signal Class will handle all logic
    /// </summary>
    public class TrafficSignal  {
      
        //Default Time Interval for Signals
        private static int _mTimeInterval = 15000;

        #region Private Variable declaration
        private int _mTotalTrafficSignal;
        private bool _mIsUTurnAllowed;
        private bool _mAnyPedeStarians;
        private bool _mThroughTrafic;
        private bool _mIsLeftSignalOn;
        private bool _mLeftSignal;
        #endregion

        public Utitlity.SignalStatus Status = ConsoleApplication3.Utitlity.SignalStatus.RED;
        public Utitlity.SignalStatus RightSignalStatus = ConsoleApplication3.Utitlity.SignalStatus.RED;
        public static event EventHandalerX RollTraffic;

        #region Constructor to assign default values
        /// <summary>
        /// Constructor for Signal Class
        /// </summary>
        /// <param name="iTotalSignals">No of Signal, Front Left turn,Right turn</param>
        /// <param name="bUturnAllowed"> U Turn allowed False or true</param>
        /// <param name="bAnyPedeStarians">if any PedeStarian on road</param>
        /// <param name="bThroughTraffic">IFor Left turn,If its Through Traffic</param>
        /// 
        public TrafficSignal(int iTotalSignals=1,bool bUturnAllowed=true,bool bAnyPedeStarians=true,bool bThroughTraffic=true,bool bIsLeftSignal = false,bool bLeftSignal = false) {
            _mAnyPedeStarians = bAnyPedeStarians;
            _mIsUTurnAllowed = bUturnAllowed;
            _mTotalTrafficSignal = iTotalSignals;
            _mThroughTrafic = bThroughTraffic;
            _mIsLeftSignalOn = bIsLeftSignal;
            _mLeftSignal = bLeftSignal;
        }
        #endregion

        #region Move forward for green light
        /// <summary>
        /// Move forward in case of Green Signal. This will check for Left,right and U Turn same time.
        /// </summary>
        /// <param name="objTS"></param>
        public static void MoveForward(TrafficSignal objTS) {
            objTS.Status = ConsoleApplication3.Utitlity.SignalStatus.GREEN;
            Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.GreenSignal));
            Console.WriteLine();
            CheckLeftTurn(objTS);
            Console.WriteLine();
            checkPedeStarian(objTS);
            Console.WriteLine();  
 
            Thread.Sleep(_mTimeInterval);
        }
        #endregion

        #region Check for PedeStarian on right
        public static void checkPedeStarian(TrafficSignal objTS) {

            if (objTS._mAnyPedeStarians)
            {
                Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.Pedesterian));
                Console.WriteLine();
                Thread.Sleep(_mTimeInterval / 2);
                Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.NOPedestarian));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.NOPedestarian));
                Console.WriteLine();
            }
         }
        #endregion

        #region check for left / U rurn
        /// <summary>
        /// This function will check for U turn and Left turn
        /// </summary>
        /// <param name="objTS"></param>
        public static void CheckLeftTurn(TrafficSignal objTS) {
            if (objTS.Status == Utitlity.SignalStatus.GREEN)
            {
                if (objTS._mIsUTurnAllowed && objTS._mThroughTrafic)
                {
                    Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions((ConsoleApplication3.Utitlity.ErrorMessage.ThroughTraffic)));
                    Console.WriteLine();
                    objTS._mThroughTrafic = false;
                }
                else if (!objTS._mThroughTrafic)
                {
                    Console.WriteLine();
                  //  Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.NoThroughTraffic));
                    if (objTS._mIsLeftSignalOn && objTS._mLeftSignal)
                    {
                        if(objTS._mIsUTurnAllowed)
                            Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.LeftUSignal));
                        else 
                            Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.LeftTurnOnly));
                    }
                    else if (objTS._mIsLeftSignalOn && !objTS._mLeftSignal)
                        Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.NoLeftTurn));
                    else {
                        Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.LeftTurnNotAllowed));
                        
                    }
                }
                else if (objTS._mThroughTrafic)
                    {
                        if (!objTS._mIsUTurnAllowed)
                            Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.LeftTurnOnly));   
                    }
                else if(objTS._mIsUTurnAllowed)
                    Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.NoUTurn));

                Console.WriteLine();
            }
        }
        #endregion

        #region Slow Down ... Yellow Light
        /// <summary>
        /// Yellow Light ... Slow Down
        /// </summary>
        /// <param name="objTS"></param>
        public static void SlowDown(TrafficSignal objTS) {
            Console.Beep();
            objTS.Status = ConsoleApplication3.Utitlity.SignalStatus.YELLOW;
            Console.WriteLine();
            Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.YellowSignal));
            Thread.Sleep(_mTimeInterval/2);

        }
        #endregion

        #region Current Stats for all variables
        public static void currentStatus(TrafficSignal objTS) {
            
            Console.WriteLine();
            Console.WriteLine("Traffic Signal Application is Starting.....");
            Console.WriteLine();
            Console.WriteLine("............Initial Stats.............");
            Console.WriteLine("Current SIGNAL            : {0}    ", objTS.Status.ToString());
            Console.WriteLine("U turn Allowed value      : {0} ", objTS._mIsUTurnAllowed.ToString());
            Console.WriteLine("Left Turn light available : {0} ", objTS._mIsLeftSignalOn);
            Console.WriteLine("Left Signal Value         : {0} " ,objTS._mLeftSignal.ToString());
            Console.WriteLine("Through Traffic           : {0}", objTS._mThroughTrafic.ToString());
            Console.WriteLine("Any Pedestrain on Right   : {0} ", objTS._mAnyPedeStarians.ToString());
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            Console.Beep();
            Thread.Sleep(_mTimeInterval / 5);
        }
        #endregion

        #region Begin Process
        public static void Begin(TrafficSignal objTS) {
            Console.WriteLine();
            Console.WriteLine("Current Signal is {0}", objTS.Status.ToString());
            Thread.Sleep(_mTimeInterval);
            Console.Beep();
        }

        #endregion

        #region Stop Car .. Red Signal
        public static void StopCar(TrafficSignal objTS)
        {
            objTS.Status = ConsoleApplication3.Utitlity.SignalStatus.RED;
            Console.WriteLine();
            Console.WriteLine(ConsoleApplication3.Utitlity.getEnumDescriptions(ConsoleApplication3.Utitlity.ErrorMessage.RedSignal));
            Console.WriteLine();

            Console.WriteLine("Wait for signal to get GREEN");
            Console.WriteLine();
            Console.Beep();

            Thread.Sleep(_mTimeInterval);
            Console.Clear();
            RollTraffic -= new EventHandalerX(Begin);
        }
        #endregion

        #region change value
        public static void ChangeValue(TrafficSignal objTS) {

            objTS._mIsLeftSignalOn = !objTS._mIsLeftSignalOn;
            objTS._mLeftSignal = !objTS._mLeftSignal;
            objTS._mIsUTurnAllowed = !objTS._mIsUTurnAllowed;
            objTS._mAnyPedeStarians = !objTS._mAnyPedeStarians;    
/*
            objTS._mIsLeftSignalOn = false;
            objTS._mLeftSignal = false;
            objTS._mIsUTurnAllowed = false;
 */
            RollTraffic.Invoke(objTS);
        }
        #endregion

        #region Invoke Method for Event Handaler
        public static void Invokemethod(TrafficSignal obj)
        {

            RollTraffic = new EventHandalerX(currentStatus);
            RollTraffic += new EventHandalerX(Begin);
            RollTraffic += new EventHandalerX(MoveForward);
            RollTraffic += new EventHandalerX(SlowDown);
            RollTraffic += new EventHandalerX(StopCar);
            RollTraffic += new EventHandalerX(ChangeValue);

            RollTraffic.Invoke(obj);
        }
        #endregion

    }
}
