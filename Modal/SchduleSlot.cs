using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendCase.Modal
{
    public class SchduleSlot
    {
        public List<Activities> activityList { get; set; }
        public List<Employees> EmployeesList { get; set; }
        public List<ScheduleBreaks> ScheduleBreaksList { get; set; }
        public List<scheduleLeaves> scheduleLeavesList { get; set; }
        public List<scheduleShifts> scheduleShiftsList { get; set; }
    }
}