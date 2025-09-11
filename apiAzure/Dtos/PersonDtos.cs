using System.ComponentModel.DataAnnotations;

namespace apiAzure.Dtos
{
    public class PersonCreateDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(50)]
        public string IpAddress { get; set; } = string.Empty;

        [StringLength(100)]
        public string HouseAddress { get; set; } = string.Empty;
    }

    public class PersonUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(50)]
        public string IpAddress { get; set; } = string.Empty;

        [StringLength(100)]
        public string HouseAddress { get; set; } = string.Empty;
    }

    public class PersonReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string HouseAddress { get; set; } = string.Empty;
    }
}


