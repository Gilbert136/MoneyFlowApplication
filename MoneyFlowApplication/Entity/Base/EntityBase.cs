using System.ComponentModel.DataAnnotations;

namespace MoneyFlowApplication.Entity.Base
{
    public class EntityBase
    {
        /// <summary>Universally unique identifier of the customer</summary>
        public virtual int Id { get; set; }
        /// <summary>User that created the record.</summary>
        [Required]
        [StringLength(150, ErrorMessage = "Created By cannot be more than 150 characters.")]
        public virtual string CreatedBy { get; set; }

        /// <summary>User that modified the record.</summary>
        [StringLength(150, ErrorMessage = "Modified By cannot be more than 150 characters.")]
        public virtual string? ModifiedBy { get; set; }

        /// <summary>Date record was created</summary>
        [Required]
        public virtual DateTime CreatedDate { get; set; }

        /// <summary>Date record was modified</summary>
        public virtual DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Logical Delete record by flagging true for delete and false for restore
        /// </summary>
        public virtual bool LogicalDelete { get; set; }
    }
}
