﻿namespace SocialNetwork.BLL.DTO.Auth.Request;

public class SignUpPostDto
{
    public string Email { get; set; } = null!;       
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}