using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    private static int startHour = 11;
    private static int startMinute = 0;

    public static int minutesToMilitaryHour(int minutes)
    {
        int totalHour = startHour + ((minutes + startMinute) / 60);
        return totalHour % 24;
    }

    public static int minutesToHourlyMinute(int minutes)
    {
        int totalMinute = startMinute + minutes;
        return totalMinute % 60;
    }
}
