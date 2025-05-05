using Domain.Models;

namespace Domain.Communication;
public class UserResponse : BaseResponse 
{
    public User User { get; private set; }
    public UserResponse(bool success, string message, User user) : base(success, message) 
    {
        User = user;
    }

    public UserResponse(bool success, string message) : this(success, message, null) { }

}
