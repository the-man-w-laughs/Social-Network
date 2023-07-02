namespace SocialNetwork.DAL;

public static class Constants
{    
    public const int ChatNameMaxLength = 45;    
    public const int CommunityNameMaxLength = 45;
    public const int CommunityDescriptionMaxLength = 500;
    public const int MessageContentMaxLength = 65535;    
    public const int CommentContentMexLength = 65535;
    public const int PostContentMaxLength = 65535;

    public const int UserLoginMinLength = 1;
    public const int UserLoginMaxLength = 20;
    public const int UserEmailMinLength = 1;
    public const int UserEmailMaxLength = 100;
    public const int UserPasswordMinLength = 6;
    public const int UserPasswordMaxLength = 50;
    
    public const int UserNameMaxLength = 20;
    public const int UserSurnameMaxLength = 40;
    public const int CountryNameMaxLength = 40;   
    public const int UserEducationMaxLength = 40;
    public const int UserSexMaxLength = 20;        

    public const int SaltMaxLength = 20;
    public const int MediaFileNameMaxLength = 255;
    public const int MediaFilePathMaxLength = 1024;
}