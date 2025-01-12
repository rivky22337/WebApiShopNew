namespace DTO
{
  public record ReturnUserDTO(int UserId,string UserName,string FirstName,string LastName);
    public record LoginUserDTO(string UserName, string Password);
    public record FullUserDTO(string UserName, string FirstName, string LastName,string Password);
}
