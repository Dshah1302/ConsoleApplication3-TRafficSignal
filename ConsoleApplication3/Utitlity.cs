using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace ConsoleApplication3
{
    public static class Utitlity
    {

        public enum ErrorMessage
        {
            [Description("It's A  RED signal, Please STOP.")]
            RedSignal,
            [Description("There is a YELLOW signal, Please Slow Down.")]
            YellowSignal,
            [Description("It's A GREEN signal, Car can move forward.")]
            GreenSignal,
            [Description("There are pedestrains on RightSide Walk, Please Wait.")]
            Pedesterian,
            [Description("There are no pedestrain now on RightSide Walk, Please move forward for turn.")]
            NOPedestarian,
            [Description("For Taking Left Or U Turn, Please Move forward. Its Through Trafic. Check for pedestarian while turning Left.")]
            ThroughTraffic,
            [Description("Please check Left sign light for taking Left Turn.")]
            NoThroughTraffic,
            [Description("NO U TURN ALLOWED.")]
            NoUTurn,
            [Description("You are allowed to take Left or U Turn Now, Check for pedestarian while turning Left.")]
            LeftUSignal,
            [Description("Left Signal sign is RED, You are not allowed to take Left or U Turn Now.")]
            NoLeftTurn,
            [Description("Left Signal is ON, You are allowed to take Left Turn Now.")]
            LeftTurnOnly,
            [Description("Left Turn is NOT ALLOWED.")]
            LeftTurnNotAllowed
        }
        public enum SignalStatus
        {
            RED,
            YELLOW,
            GREEN
        }

        /// <summary>
        /// Helper method for getting Descirption value using Reflection
        /// </summary>
        /// <param name="emEnum"></param>
        /// <returns></returns>
        public static String getEnumDescriptions(ErrorMessage emEnum)
        {
            String sMessage = String.Empty;
            FieldInfo fi = emEnum.GetType().GetField(emEnum.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                sMessage = attributes[0].Description.ToString();
            else
                sMessage = "UNKNOWN MESSAGE REQUIRED - ER01";
            
            return sMessage;
        }
    }
}
