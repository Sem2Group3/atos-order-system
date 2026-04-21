namespace Core.Security;

public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Manager = "Manager";

    public static readonly List<string> All =
    [
        Admin,
        User,
        Manager
    ];
}