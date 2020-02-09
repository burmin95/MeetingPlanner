using MeetingPlanner.Services;
using MeetingPlanner.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MeetingPlanner.Commands
{
    public class ExportMeetingsCommand : CommandBase
    {
        private readonly MeetingRepository _meetingRepository;

        public ExportMeetingsCommand(MeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public override string Description => "Экспорт встреч в файл";

        public override void DoAction()
        {
            var date = ConsoleReaders.ReadDate("Выберите день для экспорта",
                proposedDateFunc: () => DateTime.Now).Date;

            var meetings = _meetingRepository.GetMeetings(m => m.StartDate.Date == date);
            if (!meetings.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Встреч за выбранный день не найдено");
                Console.ResetColor();
                return;
            }

            var filePath = Path.Combine(Environment.CurrentDirectory, "export.txt");
            using var fileStream = File.Create(filePath);
            foreach (var meeting in meetings)
            {
                fileStream.Write(Encoding.UTF8.GetBytes(meeting.ToString()));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Экспортировано {meetings.Count} встреч в файл: {filePath}");
            Console.ResetColor();
        }
    }
}