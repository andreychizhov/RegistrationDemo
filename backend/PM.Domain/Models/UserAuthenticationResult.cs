namespace PM.Domain.Models
{
  public class UserAuthenticationResult
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public bool IsSuccess { get; set; }
    }
}