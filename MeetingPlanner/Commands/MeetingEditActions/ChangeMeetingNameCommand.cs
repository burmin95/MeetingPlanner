using System.Collections.Generic;
using MeetingPlanner.Models;
using MeetingPlanner.Utilities;

namespace MeetingPlanner.Commands.MeetingEditActions
{
    public class ChangeMeetingNameCommand : EditMeetingActionBase
    {
        public override string Description => "Изменить имя встречи";
        public override void DoAction(Meeting meeting)
        {
            var newName = ConsoleReaders.ReadString("Введите имя встречи",
                new List<ValidationRule<string>>
                {
                    new ValidationRule<string>(s => !string.IsNullOrEmpty(s), "Имя встречи не может быть пустым")
                });

            meeting.Name = newName;

            ConsoleUtils.WriteSuccess("Имя встречи изменено");
        }
    }
}