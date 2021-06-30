using BackendCase.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackendCase.DBCommunicator;

namespace BackendCase.Controllers
{

    public class ScheduleController : ApiController
    {
        DBHandler dbaccess = new DBHandler();
        public SchduleSlot GetActivityDetail()
        {
              
            DateTime Date = new DateTime(2022, 01, 01);
            SchduleSlot schduleSlot = new SchduleSlot();

            schduleSlot = dbaccess.GetActivities(Date);


            foreach (var Employees in schduleSlot.EmployeesList)
            {
                var empShifts = schduleSlot.scheduleShiftsList.Where(x => x.EmployeeID == Employees.id).ToList();


                var leavesList = schduleSlot.scheduleLeavesList.Where(x => x.EmployeeID == Employees.id).ToList();
                if (leavesList.Count > 0)
                {
                    for (int i = 0; i < leavesList.Count(); i++)
                    {
                        var leaves = leavesList[i];

                        var leaveStartDate = leaves.StartDateTime;
                        var leaveEndDate = leaves.EndDateTime;

                        var adjustShiftsEndTime = empShifts.FindAll(item => (Convert.ToDateTime(item.StartDateTime) < Convert.ToDateTime(leaveStartDate)) && (Convert.ToDateTime(item.EndDateTime) > Convert.ToDateTime(leaveStartDate)));
                        // adjusting end time
                        foreach (var item in adjustShiftsEndTime)
                        {
                            var index = schduleSlot.scheduleShiftsList.FindIndex(s => s.id == item.id);

                            schduleSlot.scheduleShiftsList[index].EndDateTime = leaveStartDate;
                        }

                        var adjustShiftsStartTime = empShifts.FindAll(item => (Convert.ToDateTime(item.StartDateTime) < Convert.ToDateTime(leaveEndDate)) && (Convert.ToDateTime(item.EndDateTime) > Convert.ToDateTime(leaveEndDate)) );
                        // adjusting start time
                        foreach (var item in adjustShiftsStartTime)
                        {
                            var index = schduleSlot.scheduleShiftsList.FindIndex(s => s.id == item.id);

                            schduleSlot.scheduleShiftsList[index].StartDateTime = leaveEndDate;
                        }




                    }



                }

            
            }



            return schduleSlot;
        }
    }
}
