﻿using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.DTO.Messages.Response;

public class MessageResponseDto
{
    public uint Id { get; set; }
    public uint ChatId { get; set; }
    public uint SenderId { get; set; }
    public uint? RepliedMessageId { get; set; }
    public string? Content { get; set; }
    public List<MediaResponseDto> Attachments { get; set; } = new List<MediaResponseDto>();
    public int? LikeCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }    
}