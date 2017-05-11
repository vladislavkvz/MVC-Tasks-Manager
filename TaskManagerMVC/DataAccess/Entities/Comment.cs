namespace DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public int TaskId { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
    }
}