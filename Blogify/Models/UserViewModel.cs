namespace Blogify.Models;

public class UserViewModel
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

    public UserViewModel(string userName, string firstName, string secondName, string status, string description)
    {
        UserName = userName;
        FirstName = firstName?.ToString();
        SecondName = secondName?.ToString();
        Status = status?.ToString();
        Description = description?.ToString();
    }
}