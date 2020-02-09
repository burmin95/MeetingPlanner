using MeetingPlanner.Services;
using System;
using System.Linq;

namespace MeetingPlanner.Commands
{
    public class ShowMeetingsCommand : CommandBase
    {
        public override string Description => "Просмотр встреч";

        private readonly MeetingRepository _meetingRepository;

        public ShowMeetingsCommand(MeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public override void DoAction()
        {
            var futureMeetings = _meetingRepository
                .GetMeetings()
                .OrderBy(m => m.StartDate)
                .ToList();

            if (!futureMeetings.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Встречи не найдены");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Встречи:");
            foreach (var futureMeeting in futureMeetings)
            {
                Console.WriteLine($"- {futureMeeting}");
            }
        }
    }
}