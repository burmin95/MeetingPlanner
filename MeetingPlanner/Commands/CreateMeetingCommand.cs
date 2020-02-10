using MeetingPlanner.Models;
using MeetingPlanner.Services;
using MeetingPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingPlanner.Commands
{
    public class CreateMeetingCommand : CommandBase
    {
        private readonly MeetingRepository _meetingRepository;
        private readonly MeetingInteractor _meetingInteractor;

        public CreateMeetingCommand(MeetingRepository meetingRepository,
            MeetingInteractor meetingInteractor)
        {
            _meetingRepository = meetingRepository;
            _meetingInteractor = meetingInteractor;
        }

        public override string Description => "Создание встречи";

        public override void DoAction()
        {
            var meetingName = ConsoleReaders.ReadString("Введите название встречи",
                new List<ValidationRule<string>>
                {
                    new ValidationRule<string>(s => !string.IsNullOrWhiteSpace(s), "Имя встречи не может быть пустым"),
                    new ValidationRule<string>(s => !_meetingRepository.GetMeetings(m => m.Name == s).Any(),
                        "Встреча с данным названием уже существует")
                });

            var startDate = ConsoleReaders.ReadDateTime("Введите начало встречи",
                new List<ValidationRule<DateTime>>
                {
                    new ValidationRule<DateTime>(d => d > DateTime.Now, "Дата начала встречи не может быть в прошлом")
                });

            var endDate = ConsoleReaders.ReadDateTime("Введите окончание встречи",
                new List<ValidationRule<DateTime>>
                {
                    new ValidationRule<DateTime>(d => d >= startDate, "Дата окончания встречи не может быть раньше начала")
                }, () => startDate.Date);

            var meeting = new Meeting(meetingName, startDate, endDate);
            if (!_meetingInteractor.CheckIntersect(meeting.StartDate, meeting.EndDate))
                return;

            _meetingRepository.AddMeeting(meeting);

            var reminder = TryAddReminder(meeting);
            if (reminder != null)
                meeting.Reminders.Add(reminder);

            ConsoleUtils.WriteSuccess($"Встреча \"{meeting}\" создана.");
        }

        private static Reminder TryAddReminder(Meeting meeting)
        {
            var needAddReminder = ConsoleReaders.ReadString("Добавить напоминание для встречи? Y - да, N/[ВВОД] - нет",
                new List<ValidationRule<string>>
                {
                    new ValidationRule<string>(s =>
                            string.IsNullOrEmpty(s) ||
                            (s.ToLower() == "n") ||
                            (s.ToLower() == "y"),
                        "Неверное значение. Y - да, N/[ВВОД] - нет")
                });

            if (needAddReminder.ToLower() == "n" || string.IsNullOrEmpty(needAddReminder))
                return null;

            var reminder = ReminderInteractor.CreateReminder(meeting);
            return reminder;
        }
    }
}