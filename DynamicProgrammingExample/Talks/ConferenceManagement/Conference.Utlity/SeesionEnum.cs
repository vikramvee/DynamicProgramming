using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Utility
{
    /// <summary>
    /// This is the session enum which is used to hold the different type of the seesions in a track. We can add or remove
    /// the new session from the enum. We can also change the attributes of the session as per our needs.
    /// </summary>
    public enum SessionEnum
    {
        [SessionInfo(1, 180, true, false)]
        MorningSession,
        [SessionInfo(2, 60, false, false)]
        Lunch,
        [SessionInfo(3, 180, true, false)]
        EveningSession,
        [SessionInfo(4, 60, true, true)]
        Networking
    }



    public class SessionInfo : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order">Order in which this part of the session is present in the track</param>
        /// <param name="duration">in minutes for the Session</param>
        /// <param name="isTalkSession">Can this part of the track contains talks? if yes, then true otherwise false</param>
        /// <param name="isExtraTime">Can this session be used to accomodate the extra talks along with the previous session</param>
        internal SessionInfo(int order, int duration, bool isTalkSession, bool isExtraTime)
        {
            this.Order = order;
            this.Duration = new TimeSpan(0, duration, 0);
            this.IsExtraTime = isExtraTime;
            this.IsTalkSession = isTalkSession;
        }


        public int Order { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsTalkSession { get; set; }
        public bool IsExtraTime { get; set; }
    }

    public class SessionHelper
    {
        public static object GetSesionAttributes(Enum value, string attributeProperty)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            SessionInfo[] attributes =
                (SessionInfo[])fi.GetCustomAttributes(
                typeof(SessionInfo),
                false);

            if (attributeProperty.Equals(Constants.Duration))
                return attributes[0].Duration;
            else if (attributeProperty.Equals(Constants.IsTalkSession))
                return attributes[0].IsTalkSession;
            else if (attributeProperty.Equals(Constants.IsExtraTime))
                return attributes[0].IsExtraTime;
            else if (attributeProperty.Equals(Constants.Order))
                return attributes[0].Order;
            else return null;
        }
    }


    public class Constants
    {
        public const string Duration = "Duration";
        public const string IsTalkSession = "IsTalkSession";
        public const string IsExtraTime = "IsExtraTime";
        public const string Order = "Order";
    }
}
