using APICatalogue.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Product : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(80, ErrorMessage = "The name must have maximum 1 and minimum 2 character")]
        [FirstLetterM]
        public string? Name { get; set; }
        [Required]
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                var firstLetter = this.Name![0].ToString();

                if (firstLetter.ToUpper() != firstLetter)
                {
                    yield return new ValidationResult("The first letter must be uppercase.", new[] { nameof(this.Name) });
                }
            }

            if(this.Stock <= 0)
            {
                yield return new ValidationResult("The stock must be greater than zero.", new[] { nameof(this.Stock) });
            }
        }
    }
}
