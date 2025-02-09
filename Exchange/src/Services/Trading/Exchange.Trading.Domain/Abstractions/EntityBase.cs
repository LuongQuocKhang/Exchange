using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exchange.Trading.Domain.Abstractions;


public class EntityBase<T>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Required]
    public required T Id { get; set; }

    #region Tracking
    public DateTime? CreatedDate { get; set; }
    public int CreatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    #endregion
}
