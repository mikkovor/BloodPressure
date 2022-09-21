namespace BloodPressure.Domain.Models
{
    public class BaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
