using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend
{
    public class Bank
    {
        [Key]
        [Column(TypeName = "nvarchar2(4)")]
        public string BANKCODE { get; set; }

        [Column(TypeName = "nvarchar2(100)")]
        public string BANKNAME { get; set; }
        [Column(TypeName = "nvarchar2(50)")]

        public string BANKACCOUNT { get; set; }
        [Column(TypeName = "number(5,2)")]

        public int? TRANSACTIONFEE { get; set; }
        [Column(TypeName = "nvarchar2(4)")]

        public string VOUCHERTYPECODE { get; set; }
        [Column(TypeName = "nvarchar2(32)")]

        public string SERVICECODE { get; set; }
        [Column(TypeName = "number(6)")]

        public int? BANKFEEID { get; set; }
        [Column(TypeName = "nvarchar2(100)")]

        public string BANKNAMEABB { get; set; }
        [Column(TypeName = "nvarchar2(100)")]

        public string BANKNAMEABBENG { get; set; }
        [Column(TypeName = "nvarchar2(512)")]

        public string BANKDESCRIPTION { get; set; }
        [Column(TypeName = "nvarchar2(512)")]

        public string BANKDESCRIPTIONENG { get; set; }
        [Column(TypeName = "nvarchar2(2)")]

        public string BANKFILETYPE { get; set; }
        [Column(TypeName = "nvarchar2(8)")]

        public string BANKFILECODE { get; set; }
        [Column(TypeName = "number(2)")]

        public int? ORDERNO { get; set; }
        [Column(TypeName = "nvarchar2(255)")]

        public string IMAGEURL { get; set; }
    }
}