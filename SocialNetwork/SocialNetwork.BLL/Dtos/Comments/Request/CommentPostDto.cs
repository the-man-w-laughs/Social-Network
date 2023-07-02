﻿namespace SocialNetwork.BLL.DTO.Comments.Request;

public class CommentPostDto
{
    public uint PostId { get; set; }
    public uint? RepliedCommentId { get; set; }
    public string? Content { get; set; }
    public List<uint>? Attachments { get; set; }
}