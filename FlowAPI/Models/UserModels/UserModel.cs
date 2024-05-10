namespace FlowApi.Models.UserModels
{
    public class UserModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool SuperAdmin { get; set; }
    }
}