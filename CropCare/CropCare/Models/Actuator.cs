namespace CropCare.Models
{
    public static class Actuator
    {
        public static string FAN = "fan";
        public static string LED = "led";
        public static string BUZZER = "buzzer";
        public static string SERVO = "servo";

        public static bool StringToBool(string value)
        {
            return value == "1" || value.ToUpper() == "ON";
        }
    }
}
