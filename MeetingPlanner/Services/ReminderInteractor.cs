using MeetingPlanner.Models;
using MeetingPlanner.Utilities;
using System;
using System.Collections.Generic;

namespace MeetingPlanner.Services
{
    public static class ReminderInteractor
    {
        public static Reminder CreateReminder(Meeting meeting)
        {
            var reminderDate = ConsoleReaders.ReadDateTime("Введите дату напоминания",
                new List<ValidationRule<DateTime>>
                {
                    new ValidationRule<DateTime>(d => d < meeting.StartDate, "Дата напоминания не может быть после начала встречи")
                }, () => meeting.StartDate.Date);

            return new Reminder(meeting, reminderDate);
        }
    }
}