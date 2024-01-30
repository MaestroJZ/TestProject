namespace TestProject.Models
{
    public class Radio
    {
        public int Id { get; set; }
        public int NumberRadio { get; set; }
        public string Mine { get; set; } = "";
        public string WorkerName { get; set; } = "";
        public int UserId { get; set; }
        public string TableNmbrAndPlace { get; set; } = "";
        public bool RadioStatus { get; set; }
        public bool IsDeleted { get; set; } = true;
    }
}
