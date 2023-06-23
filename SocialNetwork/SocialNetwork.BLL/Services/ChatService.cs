using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Repositories.Chats;

namespace SocialNetwork.BLL.Services;

public class ChatService : IChatService
{
    private readonly IMapper _mapper;
    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageMediaRepository _messageMediaRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IMediaRepository _mediaRepository;

    public ChatService(
        IMapper mapper,
        IChatRepository chatRepository,
        IChatMemberRepository chatMemberRepository,
        IMessageRepository messageRepository,
        IMessageMediaRepository messageMediaRepository,
        IMediaRepository mediaRepository)
    {
        _mapper = mapper;
        _mediaRepository = mediaRepository;
        _chatRepository = chatRepository;
        _chatMemberRepository = chatMemberRepository;
        _messageRepository = messageRepository;
        _messageMediaRepository = messageMediaRepository;
    }

    public async Task<ChatResponseDto?> GetChatById(uint chatId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var chatResponseDto = _mapper.Map<ChatResponseDto>(chat);
        chatResponseDto.UserCount = (await _chatMemberRepository.GetAllAsync(
            cm => cm.ChatId == chatId)).Count();
        return chatResponseDto;
    }

    public async Task<ChatMember> GetChatOwnerByChatId(uint chatId)
    {
        return (await _chatMemberRepository.GetAsync(
            cm => cm.ChatId == chatId && cm.TypeId == ChatMemberType.Owner))!;
    }

    public async Task DeleteChat(uint chatId)
    {
        await _chatRepository.DeleteById(chatId);
        await _chatRepository.SaveAsync();
    }

    public async Task<bool> IsUserHaveChatAdminPermissions(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var chatMember = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        return chatMember?.TypeId is ChatMemberType.Admin or ChatMemberType.Owner;
    }

    public async Task<ChatMember?> DeleteChatMember(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var memberToDelete = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        if (memberToDelete != null)
        {
            await _chatMemberRepository.DeleteById(memberToDelete.Id);
            await _chatMemberRepository.SaveAsync();
        }
        return memberToDelete;
    }

    public async Task<bool> IsUserChatMember(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var chatMember = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        return chatMember != null;
    }

    public async Task<ChatMember?> GetChatMember(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var chatMember = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        return chatMember;
    }

    public async Task<List<ChatMember>> GetAllChatMembers(uint chatId, int limit, int currCursor)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        return chat!.ChatMembers.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<List<Message>> GetAllChatMessages(uint chatId, int limit, int nextCursor)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        return chat!.Messages.OrderBy(m => m.Id)
            .Where(p => p.Id > nextCursor)
            .Take(limit)
            .ToList();
    }
    public async Task<Chat> AddChat(Chat newChat)
    {
        var chat = await _chatRepository.AddAsync(newChat);
        await _chatRepository.SaveAsync();
        return chat;
    }

    public async Task<ChatMember> AddChatMember(ChatMember chatMember)
    {
        var newChatMember = await _chatMemberRepository.AddAsync(chatMember);
        await _chatMemberRepository.SaveAsync();
        return newChatMember;
    }

    public async Task<Message> AddMessage(Message message)
    {
        var newMessage = await _messageRepository.AddAsync(message);
        await _messageRepository.SaveAsync();
        return newMessage;
    }

    public async Task<List<MediaResponseDto>> GetAllChatMedias(uint chatId, int limit, int nextCursor)
    {
        var messageMedias = await _messageMediaRepository.GetAllAsync(messageMedia => messageMedia.ChatId == chatId);

        var mediaIds = messageMedias.Select(messageMedia => messageMedia.MediaId).ToList();

        var mediaList = (await _mediaRepository.GetAllAsync(media => mediaIds.Contains(media.Id))).OrderBy(m => m.Id)
            .Where(p => p.Id > nextCursor)
            .Take(limit)
            .ToList();
        return _mapper.Map<List<MediaResponseDto>>(mediaList);
    }

    public async Task<ChatResponseDto> ChangeChat(uint chatId, ChatPatchRequestDto chatPatchRequestDto)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        bool updated = false;
        if (chatPatchRequestDto.ChatPictureId != null)
        {   
            var media = await _mediaRepository.GetByIdAsync((uint)chatPatchRequestDto.ChatPictureId);
            if (media == null)
                throw new Exception($"Media with id equal {chatPatchRequestDto.ChatPictureId} doesn't exist.");
            else
            {
                if (chat.ChatPictureId != chatPatchRequestDto.ChatPictureId)
                {
                    chat.ChatPictureId = chatPatchRequestDto.ChatPictureId;
                    updated = true;
                }                
            }
        }
        if (chatPatchRequestDto.Name != null)
        {
            if (chatPatchRequestDto.Name.Length == 0)
                throw new Exception($"Chat name should have at east one character.");
            else
            {
                if (chat.Name != chatPatchRequestDto.Name)
                {
                    chat.Name = chatPatchRequestDto.Name;
                    updated = true;
                }                
            }
        }        
        if (updated)
        {
            chat!.UpdatedAt = DateTime.Now;
            _chatRepository.Update(chat!);
            await _chatRepository.SaveAsync();            
        }
        return _mapper.Map<ChatResponseDto>(chat);
    }
}