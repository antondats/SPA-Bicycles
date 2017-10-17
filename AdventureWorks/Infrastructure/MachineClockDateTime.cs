using System;

using AdventureWorks.Abstract;

namespace AdventureWorks.Infrastructure
{
    public class MachineClockDateTime : IDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }

        public DateTime AddMinutes(int min)
        {
            return Now.AddMinutes(min);
        }

        public TimeSpan FromMinutes(int min)
        {
            return TimeSpan.FromMinutes(min);
        }
    }
}
