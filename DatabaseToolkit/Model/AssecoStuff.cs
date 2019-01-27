using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseToolkit.Model
{
    public class AssecoStuff
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public double Ratio { get; set; }
        public DateTime StartTime { get; set; }
        [Column("Note", TypeName = "NVARCHAR")]
        public string Note { get; set; }
        [Column("Desription", TypeName = "VARCHAR")]
        public string Description { get; set; }
        public byte[] Attachment { get; set; }
    }
}
