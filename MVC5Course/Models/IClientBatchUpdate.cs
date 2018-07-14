namespace MVC5Course.Models
{
    public interface IClientBatchUpdate
    {
        int ClientId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
    }
}