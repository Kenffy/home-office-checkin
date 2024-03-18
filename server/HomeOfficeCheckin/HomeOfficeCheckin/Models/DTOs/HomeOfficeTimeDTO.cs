namespace HomeOfficeCheckin.Models.DTOs
{
    public class HomeOfficeTimeDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
