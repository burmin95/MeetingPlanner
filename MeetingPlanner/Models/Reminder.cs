using System;

namespace MeetingPlanner.Models
{
    public class Reminder
    {
        public Reminder(Meeting meeting, DateTime date)
        {
            Meeting = meeting;
            Date = date;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public Meeting Meeting { get; }
        public bool IsNotified { get; set; } = false;
        public DateTime Date { get; }

        public override string ToString()
        {
            return Date.ToString("g");
        }
    }
}