using MeetingPlanner.Commands.MeetingEditActions;
using MeetingPlanner.Services;
using MeetingPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingPlanner.Commands
{
    public class EditMeetingCommand : CommandBase
    {
        public override string Description => "Изменение встречи";

        private readonly MeetingRepository _meetingRepository;
        private readonly List<EditMeetingActionBase> _meetingEditActions;

        public EditMeetingCommand(MeetingRepository meetingRepository,
            MeetingInteractor meetingInteractor)
        {
            _meetingRepository = meetingRepository;
            _meetingEditActions = new List<EditMeetingActionBase>
            {
                new ChangeMeetingNameCommand(),
                new ChangeStartDateMeetingEditAction(meetingInteractor),
                new ChangeEndDateMeetingEditAction(meetingInteractor),
                new EditRemindersMeetingEditAction()
            };
        }

        public override void DoAction()
        {
            var futureMeetings = _meetingRepository.GetMeetings(m => m.EndDate > DateTime.Now);
            if (!futureMeetings.Any())
            {
                ConsoleUtils.WriteWarning("Будующих встреч не найдено");
                return;
            }

            ConsoleUtils.WriteInvite("Выберите встречу для изменения");
            var selectedMeeting = ConsoleUtils.ItemSelector(futureMeetings, "Некорректный номер встречи");

            ConsoleUtils.WriteInvite("Выберите действие");
            var editMeetingCommandAction = ConsoleUtils.ItemSelector(_meetingEditActions, "Некорректный номер действия");
            editMeetingCommandAction.DoAction(selectedMeeting);
        }
    }
}