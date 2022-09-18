namespace SmartLock.Model
{
    public class AuditLog
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long DoorId { get; set; }
        public long OfficeId { get; set; }
        public byte Status { get; set; }
        public DateTime EventTime { get; set; }
        public string? Comments { get; set; }

        public virtual Door Door { get; set; } = null!;
        public virtual Office Office { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
