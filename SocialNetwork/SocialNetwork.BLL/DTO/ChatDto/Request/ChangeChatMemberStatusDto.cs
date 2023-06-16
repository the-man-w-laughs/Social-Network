namespace SocialNetwork.BLL.DTO.ChatDto.Request
{
    public class ChangeChatMemberStatusDto
    {
        public enum Type { Member, Admin }
        public Type TypeId { get; set; }
    }
}