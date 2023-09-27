using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedLimitApiUser
{
    internal class UserInputHandler
    {
        public static int EnterWithLimits(int min, int max)
        {
            while (true)
            {
                int num = int.Parse(Console.ReadLine());
                if (num < min || num > max) {
                    Console.WriteLine("Number sshould be less than " + min + " and greater than " + max);
                }
                else {
                    return num;
                }
            }
        }
        public static float EnterWithLimits(float min, float max)
        {
            while (true)
            {
                int num = int.Parse(Console.ReadLine());
                if (num < min && num > max)
                {
                    Console.WriteLine("Number sshould be less than " + min + " and greater than " + max);
                }
                else
                {
                    return num;
                }
            }
        }
        private static int daysInMonth(int month)
        {
            int days = 0;
            switch (month)
            {
                case 1:  days = 31;  break;
                case 2: days = 28; break;
                case 3: days = 31; break;
                case 4: days = 30; break;
                case 5: days = 31; break;
                case 6: days = 30; break;
                case 7: days = 31; break;
                case 8: days = 31; break;
                case 9: days = 30; break;
                case 10: days = 31; break;
                case 11: days = 30; break;
                case 12: days = 31; break;
            }
            return days;
        }
        public static DateOnly EnterDate()
        {
            Console.WriteLine("Enter year");
            int year = EnterWithLimits(2020, 2023);
            Console.WriteLine("Enter month");
            int month = EnterWithLimits(1, 12);
            Console.WriteLine("Enter day");
            int day = EnterWithLimits(1, daysInMonth(month));
            return new DateOnly(year, month, day);
        }
    }
}
