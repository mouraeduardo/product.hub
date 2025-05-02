namespace Domain.Models;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeletionDate { get; set; }
}
