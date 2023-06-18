namespace SocialNetwork.DAL;

public static class Constants
{
    public static readonly Version MySqlServerVersion = new(8, 0, 32);
    
    public const int ChatNameMaxLength = 45;
    public const int CommunityNameMaxLength = 45;
    public const int MediaFileNameMaxLength = 255;
    public const int MediaFilePathMaxLength = 1024;
    public const int MessageTextMaxLength = 65535;
    public const int PostContentMaxLength = 65535;
    public const int UserLoginMaxLength = 20;
    public const int UserEmailMaxLength = 32;
    public const int CountryNameMaxLength = 40;
    public const int UserEducationMaxLength = 40;
    public const int UserSurnameMaxLength = 40;
    public const int UserSexMaxLength = 20;
    public const int UserNameMaxLength = 20;
    public const int EmailMaxLength = 100;
}