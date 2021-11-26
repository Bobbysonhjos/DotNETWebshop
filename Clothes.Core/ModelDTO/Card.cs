using System.ComponentModel.DataAnnotations;

namespace Clothes.Core.ModelDTO
{
    public class Card
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string CVV { get; set; }
        [Required]
        public string Expiration { get; set; }

    }
}
