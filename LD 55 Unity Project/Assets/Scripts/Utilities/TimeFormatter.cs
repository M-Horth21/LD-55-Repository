using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class TimeFormatter
{
    public static string GetTimeString(ulong milliseconds)
    {
        return GetTimeString(TimeSpan.FromMilliseconds(milliseconds));
    }

    public static string GetTimeString(TimeSpan timeSpan)
    {
        StringBuilder timeString = new();

        if (timeSpan.TotalHours >= 1)
        {
            timeString.Append($"{timeSpan.TotalHours:N0}h ");
        }

        if (timeSpan.Minutes >= 1)
        {
            timeString.Append($"{timeSpan.Minutes:N0}m ");
        }

        timeString.Append($"{timeSpan.Seconds + timeSpan.Milliseconds / 1000f:F1}s");

        return timeString.ToString();
    }
}