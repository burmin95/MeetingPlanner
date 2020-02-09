namespace MeetingPlanner.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract string Description { get; }
        public abstract void DoAction();

        public override string ToString()
        {
            return Description;
        }
    }
}