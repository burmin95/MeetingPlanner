using MeetingPlanner.Models;
using MeetingPlanner.Services;
using MeetingPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingPlanner.Commands.MeetingEditActions
{
    public class EditRemindersMeetingEditAction : EditMeetingActionBase
    {
        public override string Description => "Изменить напоминания";
        public override void DoAction(Meeting meeting)
        {
            if (meeting.Reminders.Any())
            {
                Console.WriteLine($"Текущие напоминания для встречи {meeting.Name}");
                foreach (var reminder in meeting.Reminders)
                {
                    Console.WriteLine($"- {reminder.Date}");
                }
            }

            ConsoleUtils.WriteInvite("Выберите действие:");
            var reminderAction = ConsoleUtils.ItemSelector(ReminderActions, "Некорректный номер действия");
            reminderAction.DoAction(meeting);
        }

        private static readonly List<EditMeetingActionBase> ReminderActions
            = new List<EditMeetingActionBase>
            {
                new AddReminderMeetingEditAction(),
                new DeleteReminderMeetingEditAction()
            };
    }

    public class AddReminderMeetingEditAction : EditMeetingActionBase
    {
        public override string Description => "Добавить напоминание";
        public override void DoAction(Meeting meeting)
        {
            var reminder = ReminderInteractor.CreateReminder(meeting);
            meeting.Reminders.Add(reminder);
            ConsoleUtils.WriteSuccess("Напоминание добавлено");
        }
    }

    public class DeleteReminderMeetingEditAction : EditMeetingActionBase
    {
        public override string Description => "Удалить напоминание";
        public override void DoAction(Meeting meeting)
        {
            if (!meeting.Reminders.Any())
            {
                ConsoleUtils.WriteError($"У встречи {meeting.Name} нет напоминаний");
                return;
            }

            ConsoleUtils.WriteSuccess("Выберите напоминание:");
            var reminderToDelete = ConsoleUtils.ItemSelector(meeting.Reminders, "Некорректный номер напоминания");
            meeting.Reminders.Remove(reminderToDelete);
            ConsoleUtils.WriteSuccess("Напоминание удалено");
        }
    }
}