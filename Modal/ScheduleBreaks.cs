using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendCase.Modal
{
    public class ScheduleBreaks
    {
        public long id { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public long ActivityID { get; set; }
        public long EmployeeID { get; set; }


    }
}