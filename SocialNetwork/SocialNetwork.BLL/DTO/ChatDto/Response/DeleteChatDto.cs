namespace SocialNetwork.BLL.DTO.ChatDto.Response
{
    public class DeleteChatDto
    {
        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
