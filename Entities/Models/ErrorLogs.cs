namespace Entities.Models
{
    public partial class ErrorLogs
    {
        public int Id { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
