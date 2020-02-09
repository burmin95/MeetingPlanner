using MeetingPlanner.Commands;
using MeetingPlanner.Jobs;
using MeetingPlanner.Services;
using MeetingPlanner.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingPlanner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Dependencies
            var meetingRepository = new MeetingRepository();
            var meetingInteractor = new MeetingInteractor(meetingRepository);

            // Jobs
            var jobs = new List<IJob>
            {
                new MeetingReminderJob(meetingRepository)
            };
            StartJobs(jobs);

            // Commands
            var commands = new List<ICommand>
            {
                new CreateMeetingCommand(meetingRepository, meetingInteractor),
                new EditMeetingCommand(meetingRepository, meetingInteractor),
                new ShowMeetingsCommand(meetingRepository),
                new ExportMeetingsCommand(meetingRepository)
            };

            while (true)
            {
                ConsoleUtils.WriteInvite("Выберите комманду");
                var command = ConsoleUtils.ItemSelector(commands, "Некорректный номер команды");
                command.DoAction();
            }
        }

        private static void StartJobs(IEnumerable<IJob> jobs)
        {
            foreach (var job in jobs)
            {
                Task.Run(() =>
                {
                    job.Start();
                });
            }
        }
    }
}