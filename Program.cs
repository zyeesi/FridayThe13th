using System.Text;

var exitCode = "n";
do
{
    Console.WriteLine("Input year to calculate Friday the 13th");
    var inputYear = int.Parse(Console.ReadLine());
    var doomsDay = GetDoomsDay(inputYear);
    var doomsDate = IsLeapYear(inputYear) ? 4 : 3; // First doomsdate Jan 3rd or Jan 4th
    // https://www.mischel.com/diary/2005/08/08.htm
    var monthDayOffset = IsLeapYear(inputYear) ? new List<int>() { 0, 4, 3, 0, 5, 2, 0, 4, 1, 6, 3, 1 } : new List<int>() { 0, 4, 4, 1, 6, 3, 1, 5, 2, 0, 4, 2 };

    var firstFriday = GetFridayDate(doomsDate, doomsDay);

    var sb = new StringBuilder();
    sb.AppendLine("-------------------------------");
    for (int i = 0; i < 12; i++)
    {
        if (firstFriday + monthDayOffset[i] == 6)
        {
            sb.AppendLine($"{(Months) i} 13th is Friday");
        }
    }
    sb.AppendLine("-------------------------------");
    sb.AppendLine("Enter 'y' to exit. Enter anything else to restart");
    Console.WriteLine(sb.ToString());

    exitCode = Console.ReadLine();
} while (exitCode != "y");

int GetFridayDate(int doomsDate, int doomsDay)
{
    if (doomsDay > 5)
    {
        return doomsDate - (doomsDay - 5);
    }
    if (doomsDay < 5)
    {
        return doomsDate + (5 - doomsDay);
    }

    return doomsDate;
}

/// https://en.wikipedia.org/wiki/Doomsday_rule#Why_it_works
int GetDoomsDay(int year)
{
    var anchorDay = GetAnchorDay(year);
    var subYear = int.Parse(year.ToString().Substring(2));
    var doomsDay = (subYear / 12 + subYear % 12 + (subYear % 12 / 4)) % 7 + (int) anchorDay;

    return doomsDay;
}

Days GetAnchorDay(int year)
{
    //if not year blah blah blah
    if (year >= 1900 && year < 1999)
    {
        return Days.Wednesday;
    }
    if (year >= 2000 && year < 2099)
    {
        return Days.Tuesday;
    }
    if (year >= 2100 && year < 2199)
    {
        return Days.Sunday;
    }

    throw new Exception("Not valid year.");
}

bool IsLeapYear(int year)
{
    if (year % 4 != 0)
    {
        return false;
    }
    if (year % 100 == 0 && year % 400 != 0)
    { 
        return false; 
    }

    return true;
}

enum Days
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7,
}

enum Months
{
    January, 
    February, 
    March, 
    April, 
    May, 
    June, 
    July, 
    August, 
    September, 
    October, 
    November, 
    December
}