using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class LableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long lableId { get; set; }
        public string lableName { get; set; }
        [ForeignKey("UserTable")]
        public long UserId { get; set; }
        [ForeignKey("NoteTable")]
        public long noteID { get; set; }
        public virtual UserEntity userEntity { get; set; }
        public virtual NoteEntity noteEntity { get; set; }
    }
}
