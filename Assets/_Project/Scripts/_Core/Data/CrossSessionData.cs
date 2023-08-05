using System;
using PrefVar = HOT.Data.PlayerPrefVariables.PrefVar;

namespace HOT.Data
{
    public class CrossSessionData
    {
        private readonly PrefVar dieTime;
        
        public CrossSessionData()
        {
            dieTime = new PrefVar("dieTime", DateTime.Now.ToUnixTimestamp() + DateUtils.SecondsInDay);
        }

        public int DieTime
        {
            get { return dieTime.Int; }
            set { dieTime.Int = value; }
        }
    }
}
