namespace PMS.Application.Common
{
    public static class GetBreak
    {
        public static string GetBreakName(int breakTypeId)
        {
            return breakTypeId switch
            {
                1 => "Meal",
                2 => "Prayer",
                3 => "Bio",
                4 => "Coaching",
                5 => "Training",
                6 => "Huddle",
                _ => "Break"
            };
        }
    }
}
