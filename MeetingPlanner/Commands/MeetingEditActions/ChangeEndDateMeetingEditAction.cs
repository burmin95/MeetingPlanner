using MeetingPlanner.Models;
using MeetingPlanner.Services;
using MeetingPlanner.Utilities;
using System;
using System.Collections.Generic;

namespace MeetingPlanner.Commands.MeetingEditActions
{
    public class ChangeEndDateMeetingEditAction : EditMeetingActionBase
    {
        private readonly MeetingInteractor _meetingInteractor;

        public ChangeEndDateMeetingEditAction(MeetingInteractor meetingInteractor)
        {
            _meetingInteractor = meetingInteractor;
        }

        public override string Description => "Изменить дату окончания";
        public override void DoAction(Meeting meeting)
        {
            var newEndData = ConsoleReaders.ReadDateTime(validationRules:
                new List<ValidationRule<DateTime>>
                {
                    new ValidationRule<DateTime>(d => d > DateTime.Now, "Дата не может быть в прошлом"),
                    new ValidationRule<DateTime>(d => d >= meeting.StartDate, "Дата начала не может быть раньше начала")
                });

            if (!_meetingInteractor.CheckIntersect(meeting.StartDate, newEndData, meeting))
                return;

            meeting.EndDate = newEndData;
            ConsoleUtils.WriteSuccess("Дата окончания изменена");
        }
    }
}