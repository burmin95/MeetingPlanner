using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MeetingPlanner.Models;
using MeetingPlanner.Services;
using MeetingPlanner.Utilities;

namespace MeetingPlanner.Jobs
{
    public class MeetingReminderJob : IJob
    {
        private readonly MeetingRepository _meetingRepository;
        private Timer _timer;

        public MeetingReminderJob(MeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public void Start()
        {
            _timer = new Timer(state => CheckMeetings(), null, 2000, 1000);
        }

        private void CheckMeetings()
        {
            var reminders = _meetingRepository
                .GetMeetings()
                .SelectMany(m => m.Reminders)
                .Where(r => !r.IsNotified && r.Date <= DateTime.Now)
                .ToList();

            if (reminders.Any())
                Remind(reminders);
        }

        private void Remind(List<Reminder> reminders)
        {
            var leftPos = Console.CursorLeft;
            var topPos = Console.CursorTop;

            Console.MoveBufferArea(0, topPos, leftPos, 1, 0, (topPos + reminders.Count + 2));

            Console.CursorLeft = 0;

            ConsoleUtils.WriteWarning("--------- Напоминание ---------");
            
            foreach (var reminder in reminders)
            {
                ConsoleUtils.WriteWarning($"- Встреча {reminder.Meeting.Name} через {Humanize(reminder.Meeting.StartDate - DateTime.Now)}");
            }

            ConsoleUtils.WriteWarning("-------------------------------");

            Console.CursorLeft = leftPos;

            reminders.ForEach(r => r.IsNotified = true);
        }

        private static string Humanize(TimeSpan timeSpan)
        {
            if (timeSpan.TotalHours >= 1)
                return $"{(int) timeSpan.TotalHours} час {timeSpan.Minutes} минут";

            return $"{timeSpan.Minutes} минут";
        }
    }
}