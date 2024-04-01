using System.Collections.Generic;
using UnityEngine;

public class ScheduleManager : ManagerBase<ScheduleManager>
{
    private List<Schedule> schedules = new();

    // private void Start()
    // {
    //     AddSchedule(new Schedule(6,null,() => Debug.Log(6)));
    //     AddSchedule(new Schedule(8,null,() => Debug.Log(8)));
    //     AddSchedule(new Schedule(3,null,() => Debug.Log(3)));
    //     AddSchedule(new Schedule(3,null,() => Debug.Log(3)));
    //     AddSchedule(new Schedule(10,null,() => Debug.Log(10)));
    // }
    
    private void Update()
    {
        if (schedules.Count == 0) return;
        if (schedules[0].endTime <= Time.realtimeSinceStartup)
            InvokeSchedule();
    }

    public void AddSchedule(Schedule schedule)
    {
        if (schedule == null) return;
        schedules.Add(schedule);
        schedules.Sort((schedule1,schedule2) => schedule1.endTime >= schedule2.endTime ? 1 : -1);
        schedule.InvokeStartCallback();
    }

    public void RemoveSchedule(Schedule schedule)
    {
        if (!schedules.Contains(schedule)) return;
        schedules.Remove(schedule);
    }

    private void InvokeSchedule()
    {
        while (schedules.Count != 0 && schedules[0].endTime <= Time.realtimeSinceStartup)
        {
            schedules[0].InvokeEndCallback();
            schedules.RemoveAt(0);
        }
    }
}