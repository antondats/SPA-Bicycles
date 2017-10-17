using System;

namespace AdventureWorks.Abstract
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime AddMinutes(int min);
        TimeSpan FromMinutes(int min);
    }
}
