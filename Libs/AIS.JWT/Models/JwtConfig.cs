namespace AIS.JWT.Models
{
    public class JwtConfig
    {
        public static readonly string SectionName = "JwtConfig";

        public string Key { get; set; }
        public int ExpiredMinutes { get; set; } = 240;
    }
}