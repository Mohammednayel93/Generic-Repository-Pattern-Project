namespace DAL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class News
    {
        [Key]
        public int Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Details { get; set; }

        [Required]
        [StringLength(200)]
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
