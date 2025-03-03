using System.ComponentModel.DataAnnotations;

namespace DTO
{
  public record ReturnUserDTO(
      [Required]int UserId,
      [Required, EmailAddress]string UserName
      ,[Required]string FirstName,
       [Required]string LastName);
    public record LoginUserDTO([Required] string UserName,
                               [Required]string Password);
    public record FullUserDTO(
              [Required, EmailAddress]string UserName, 
              [Required] string FirstName, 
              [Required] string LastName,
              [Required] string Password);
}
