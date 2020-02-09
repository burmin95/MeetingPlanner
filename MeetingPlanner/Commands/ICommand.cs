namespace MeetingPlanner.Commands
{
    public interface ICommand
    {
        string Description { get; }
        void DoAction();
    }
}