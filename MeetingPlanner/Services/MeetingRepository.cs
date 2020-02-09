using System;
using System.Collections.Generic;
using System.Linq;
using MeetingPlanner.Models;

namespace MeetingPlanner.Services
{
    public class MeetingRepository
    {
        private readonly List<Meeting> _meetings = new List<Meeting>();

        public void AddMeeting(Meeting meeting)
        {
            _meetings.Add(meeting);
        }

        public List<Meeting> GetMeetings(Func<Meeting, bool> predicate = null)
        {
            if (predicate == null)
                predicate = _ => true;

            return _meetings.Where(predicate).ToList();
        }
    }
}