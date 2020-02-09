using MeetingPlanner.Models;
using MeetingPlanner.Utilities;
using System;
using System.Linq;

namespace MeetingPlanner.Services
{
    public class MeetingInteractor
    {
        private readonly MeetingRepository _meetingRepository;

        public MeetingInteractor(MeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public bool CheckIntersect(DateTime startDate, DateTime endDate, Meeting exceptMeeting = null)
        {
            var intersectWith = _meetingRepository.GetMeetings(m =>
                    ((exceptMeeting == null) || m.Id != exceptMeeting.Id)
                    && (((m.StartDate > startDate) && (m.StartDate < endDate))
                        || ((m.EndDate > startDate) && (m.EndDate < endDate))
                        || ((m.StartDate <= startDate) && (m.EndDate >= endDate))))
                .ToList();
            if (intersectWith.Any())
            {
                ConsoleUtils.WriteError("Встреча пересекается с");
                foreach (var intersectMeeting in intersectWith)
                {
                    ConsoleUtils.WriteError(intersectMeeting.ToString());
                }

                return false;
            }

            return true;
        }
    }
}