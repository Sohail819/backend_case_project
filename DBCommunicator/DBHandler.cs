using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BackendCase.Modal;

namespace BackendCase.DBCommunicator
{
    public class DBHandler
    {
        
        SqlCommand command = null;
        public SchduleSlot GetActivities(DateTime date)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BackendConnectionString"].ConnectionString);

                SchduleSlot SchduleSlot = new SchduleSlot();
                

                command = new SqlCommand("[dbo].[GetActivities]", con);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Date", date);
                
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataSet dt = new DataSet();
                sqlDataAdapter.Fill(dt);

                if (dt != null
                            && dt.Tables != null
                            && dt.Tables.Count > 0)
                {
                    var ActivityTable = dt.Tables[0];
                    if (ActivityTable.Rows.Count>0)
                    {
                        SchduleSlot.activityList = ActivityTable.AsEnumerable().Select(row => new Activities
                        {
                            id = row.Field<Int64>("id"),
                            name = row.Field<string>("name")
                        }).ToList();
                    }
                    
                         var EmployeesTable = dt.Tables[1];
                    if (EmployeesTable.Rows.Count > 0)
                    {
                        SchduleSlot.EmployeesList = EmployeesTable.AsEnumerable().Select(row => new Employees
                        {
                            id = row.Field<Int64>("id"),
                            name = row.Field<string>("name")
                        }).ToList();
                    }

                    var BreaksTable = dt.Tables[2];
                    if (BreaksTable.Rows.Count > 0)
                    {
                        SchduleSlot.ScheduleBreaksList = BreaksTable.AsEnumerable().Select(row => new ScheduleBreaks
                        {
                            id = row.Field<Int64>("id"),
                            StartDateTime = row.Field<DateTime>("StartDateTime"),
                            EndDateTime = row.Field<DateTime>("EndDateTime"),
                            ActivityID = row.Field<Int64>("ActivityID"),
                            EmployeeID = row.Field<Int64>("EmployeeID"),

                        }).ToList();
                    }

                    var LeavesTable = dt.Tables[3];
                    if (LeavesTable.Rows.Count > 0)
                    {
                        SchduleSlot.scheduleLeavesList = LeavesTable.AsEnumerable().Select(row => new scheduleLeaves
                        {
                            id = row.Field<Int64>("id"),
                            StartDateTime = row.Field<DateTime>("StartDateTime"),
                            EndDateTime = row.Field<DateTime>("EndDateTime"),
                            ActivityID = row.Field<Int64>("ActivityID"),
                            EmployeeID = row.Field<Int64>("EmployeeID"),

                        }).ToList();
                    }

                    var ShiftsTable = dt.Tables[4];
                    if (ShiftsTable.Rows.Count > 0)
                    {
                        SchduleSlot.scheduleShiftsList = ShiftsTable.AsEnumerable().Select(row => new scheduleShifts
                        {
                            id = row.Field<Int64>("id"),
                            StartDateTime = row.Field<DateTime>("StartDateTime"),
                            EndDateTime = row.Field<DateTime>("EndDateTime"),
                            ActivityID = row.Field<Int64>("ActivityID"),
                            EmployeeID = row.Field<Int64>("EmployeeID"),

                        }).ToList();
                    }

                    
                    return SchduleSlot;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return null;
        }
    }
}