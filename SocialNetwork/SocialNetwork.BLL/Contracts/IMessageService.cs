using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IMessageService
{
    Task<MessageResponseDto> GetMessage(uint messageId);
    Task<MessageResponseDto> ReplyMessage(uint userId, uint messageId, MessageRequestDto messageRequestDto);
    Task<MessageResponseDto> DeleteMessage(uint userId, uint messageId);
    Task<List<MessageLikeResponseDto>> GetAllMessageLikesPaginated(uint messageId, int limit, int currCursor);
    Task<MessageLikeResponseDto> LikeMessage(uint userId, uint messageId);
    Task<MessageLikeResponseDto> UnlikeMessage(uint userId, uint messageId);
    Task<MessageResponseDto> ChangeMessage(uint userId, uint messageId, MessagePatchRequestDto messagePatchRequestDto);
}