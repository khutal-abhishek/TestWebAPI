using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebAPI.Model
{
    [Table("TestTable")]
    public class Test
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
