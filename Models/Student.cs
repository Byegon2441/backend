using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend
{
    public class Student
    {
        [Key]
        [Column(TypeName = "number(5)")]

        public int id { get; set; }

        [Column(TypeName = "nvarchar2(50)")]
        public string name { get; set; }
        [Column(TypeName = "nvarchar2(50)")]

        public string study { get; set; }
        [Column(TypeName = "nvarchar2(50)")]

        public string test { get; set; }
        [Column(TypeName = "nvarchar2(50)")]

        public string grade { get; set; }
        [Column(TypeName = "nvarchar2(50)")]

        public string finance { get; set; }
        [Column(TypeName = "nvarchar2(100)")]

        public string lineuserid { get; set; }


    }
}