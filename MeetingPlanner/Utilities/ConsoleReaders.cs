using MeetingPlanner.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MeetingPlanner.Utilities
{
    public static class ConsoleReaders
    {
        public static string ReadString(string inviteText, List<ValidationRule<string>> validationRules)
        {
            while (true)
            {
                ConsoleUtils.WriteInvite(inviteText);

                Console.Write("> ");
                var input = Console.ReadLine();

                if (Check(input, validationRules))
                    return input;
            }
        }

        public static int ReadInt(string inviteText = "", List<ValidationRule<int>> validationRules = null)
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(inviteText))
                    ConsoleUtils.WriteInvite(inviteText);

                Console.Write("> ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out var inputNumber))
                {
                    ConsoleUtils.WriteError("Некорректное число");
                    continue;
                }

                if (Check(inputNumber, validationRules))
                    return inputNumber;
            }
        }

        public static DateTime ReadDateTime(string inviteText = "", List<ValidationRule<DateTime>> validationRules = null,
            Func<DateTime> proposedDateFunc = null)
        {
            if (proposedDateFunc == null)
                proposedDateFunc = () => DateTime.Now;

            while (true)
            {
                if (!string.IsNullOrEmpty(inviteText))
                    ConsoleUtils.WriteInvite(inviteText);

                var date = ReadDateTime(proposedDateFunc);

                if (Check(date, validationRules))
                    return date;
            }
        }

        public static DateTime ReadDate(string inviteText, List<ValidationRule<DateTime>> validationRules = null,
            Func<DateTime> proposedDateFunc = null)
        {
            if (proposedDateFunc == null)
                proposedDateFunc = () => DateTime.Now;

            while (true)
            {
                ConsoleUtils.WriteInvite(inviteText);

                var date = ReadDate(proposedDateFunc);

                if (Check(date, validationRules))
                    return date.Date;
            }
        }

        private static bool Check<T>(T value, IEnumerable<ValidationRule<T>> validationRules)
        {
            if (validationRules == null)
                return true;

            bool isValid = true;
            foreach (var validationRule in validationRules)
            {
                if (!validationRule.Validate(value))
                {
                    isValid = false;
                    ConsoleUtils.WriteError(validationRule.ErrorText);
                }
            }

            return isValid;
        }

        private static DateTime ReadDateTime(Func<DateTime> proposedDateFunc)
        {
            var time = ReadTime();
            var date = ReadDate(proposedDateFunc).Date;

            return date + time;
        }

        private static TimeSpan ReadTime()
        {
            while (true)
            {
                ConsoleUtils.WriteInvite("Введите время:");
                Console.Write("> ");
                var timeString = Console.ReadLine();
                if (TimeSpan.TryParseExact(timeString, "h\\:m", null, out var time))
                    return time;

                ConsoleUtils.WriteError("Неверный формат времени. Введите в формате \"hh:mm\"");
            }
        }

        private static DateTime ReadDate(Func<DateTime> proposedDateFunc)
        {
            while (true)
            {
                var proposedDate = proposedDateFunc();
                ConsoleUtils.WriteInvite($"Введите дату: (Нажмите [ВВОД], чтобы оставить '{proposedDate.ToShortDateString()}')");
                Console.Write("> ");
                var dateString = Console.ReadLine();
                if (string.IsNullOrEmpty(dateString))
                    return proposedDate;

                if (DateTime.TryParse(dateString, out var date))
                    return date;

                ConsoleUtils.WriteError($"Неверный формат даты. Введите дату в формате {CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern}");
            }
        }
    }
}