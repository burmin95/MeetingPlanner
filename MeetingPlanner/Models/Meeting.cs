using System;
using System.Collections.Generic;

namespace MeetingPlanner.Models
{
    public class Meeting
    {
        public Meeting(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Reminder> Reminders { get; } = new List<Reminder>();

        public override string ToString()
        {
            return $"{Name} - с: {StartDate:g} до: {EndDate:g}";
        }
    }
}