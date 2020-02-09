using MeetingPlanner.Models;

namespace MeetingPlanner.Commands.MeetingEditActions
{
    public abstract class EditMeetingActionBase
    {
        public abstract string Description { get; }
        public abstract void DoAction(Meeting meeting);

        public override string ToString()
        {
            return Description;
        }
    }
}