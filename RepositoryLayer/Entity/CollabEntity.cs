using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long collabId { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string collabEmail { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime modifyDate { get; set; }
        [ForeignKey("UserTable")]
        public long UserId { get; set; }

        [ForeignKey("NoteTable")]
        public long noteID { get; set; }
        public virtual UserEntity userEntity { get; set; }
        public virtual NoteEntity noteEntity { get; set; }
    }
}
