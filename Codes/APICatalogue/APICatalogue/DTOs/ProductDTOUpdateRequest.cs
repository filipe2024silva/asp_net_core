using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ProductDTOUpdateRequest : IValidatableObject
    {
        [Range(1, 9999, ErrorMessage = "The stock value must be between 1 and 9999.")]
        public float Stock { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(CreatedAt.Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("The created date must be greater than today's date.", new[] { nameof(this.CreatedAt) });
            }
        }
    }
}
