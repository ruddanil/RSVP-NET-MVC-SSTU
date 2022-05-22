using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSVP_website_SSTU.Models
{
    public partial class Rsvp
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        [EmailAddress]
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$", ErrorMessage = "Not a valid email")]
        public string? Email { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; } = null!;
        public bool WillAttend { get; set; }
    }
}
