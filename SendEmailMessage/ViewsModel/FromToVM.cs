using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SendEmail.ViewsModel
{
    public class FromToVM
    {
      
        [Required(ErrorMessage = "!اكتب صح يا علق")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string To { get; set; }
        [Required(ErrorMessage = "! اكتب الموضوع")]
        [DataType(DataType.Text)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "!اهم حاجه لازم تكتبه")]
        [DataType(DataType.Text)]
        public string Body { get; set; }
        [Required(ErrorMessage ="انجز ...")]
        [DataType(DataType.ImageUrl)]
        public IList<IFormFile> Attachments { get; set; }
        
    }
}
