using System;
using System.Collections.Generic;
using MeetingPlanner.Models;
using MeetingPlanner.Services;
using MeetingPlanner.Utilities;

namespace MeetingPlanner.Commands.MeetingEditActions
{
    public class ChangeStartDateMeetingEditAction : EditMeetingActionBase
    {
        private readonly MeetingInteractor _meetingInteractor;

        public ChangeStartDateMeetingEditAction(MeetingInteractor meetingInteractor)
        {
            _meetingInteractor = meetingInteractor;
        }

        public override string Description => "Изменить дату начала";
        public override void DoAction(Meeting meeting)
        {
            var newStartDate = ConsoleReaders.ReadDateTime(validationRules:
                new List<ValidationRule<DateTime>>
                {
                    new ValidationRule<DateTime>(d => d > DateTime.Now, "Дата не может быть в прошлом"),
                    new ValidationRule<DateTime>(d => d <= meeting.EndDate, "Дата начала не может быть позже окончания")
                });

            if (!_meetingInteractor.CheckIntersect(newStartDate, meeting.EndDate, meeting))
                return;

            meeting.StartDate = newStartDate;
            ConsoleUtils.WriteSuccess("Дата начала изменена");
        }
    }
}