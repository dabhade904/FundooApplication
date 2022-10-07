using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long noteID { get; set; }
        public string title { get; set; }
        public string discription { get; set; }
        public DateTime reminder { get; set; }
        public string color { get; set; }
        public string img { get; set; }
        public bool archive { get; set; }
        public bool pin { get; set; }
        public bool trash { get; set; }
        public DateTime time_created { get; set; }
        public DateTime time_edited { get; set; }
        [ForeignKey("UserEntity")]
        public long UserId { get; set; }
        public virtual UserEntity UserEntity { get; set; }
    }
}