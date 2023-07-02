using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.Services;

public class ChatService : IChatService
{
    private readonly IMapper _mapper;
    private readonly IChatRepository _chatRepository;
    private readonly IMessageMediaRepository _messageMediaRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IMediaRepository _mediaRepository;
    
    public ChatService(IMapper mapper, IChatRepository chatRepository, IChatMemberRepository chatMemberRepository,
        IMessageMediaRepository messageMediaRepository, IMediaRepository mediaRepository)
    {
        _mapper = mapper;
        _mediaRepository = mediaRepository;
        _chatRepository = chatRepository;
        _chatMemberRepository = chatMemberRepository;
        _messageMediaRepository = messageMediaRepository;
    }

    public async Task<ChatResponseDto> CreateChat(uint userId, ChatPostDto chatRequestDto)
    {
        var newChat = new Chat { Name = chatRequestDto.Name, CreatedAt = DateTime.Now };
        
        var addedChat = await _chatRepository.AddAsync(newChat);
        await _chatRepository.SaveAsync();

        var chatOwner = new ChatMember
        {
            ChatId = addedChat.Id,
            CreatedAt = DateTime.Now,
            TypeId = ChatMemberType.Owner,
            UserId = userId
        };
        
        await _chatMemberRepository.AddAsync(chatOwner);
        await _chatMemberRepository.SaveAsync();

        return _mapper.Map<ChatResponseDto>(addedChat);
    }

    public async Task<ChatResponseDto> GetChatInfo(uint chatId, uint userId)
    {
        var chat = await GetChatById(chatId);
        var isUserChatMember = await IsUserChatMember(chatId, userId);            
        if (!isUserChatMember) 
            throw new AccessDeniedException("You are not chat member.");

        return _mapper.Map<ChatResponseDto>(chat);
    }

    public async Task<List<MediaResponseDto>> GetChatMedias(uint userId, uint chatId, int limit, int nextCursor)
    {
        var chat = await GetChatById(chatId);
        if (chat == null) 
            throw new NotFoundException("Chat with request Id doesn't exist");

        var isChatMember = await IsUserChatMember(chatId, userId);
        if (!isChatMember) 
            throw new AccessDeniedException("You are not chat member.");

        var messageMedias = await _messageMediaRepository.GetAllAsync(messageMedia => messageMedia.ChatId == chatId);
        var mediaIds = messageMedias.Select(messageMedia => messageMedia.MediaId).ToList();

        var mediaList = (await _mediaRepository.GetAllAsync(media => mediaIds.Contains(media.Id)))
            .OrderBy(m => m.Id)
            .Skip(nextCursor).Take(limit)
            .Select(m => _mapper.Map<MediaResponseDto>(m))
            .ToList();
        return mediaList;
    }

    public async Task<ChatResponseDto> UpdateChat(uint chatId, uint userId, ChatPatchDto chatPatchRequestDto)
    {
        var chat = await GetChatById(chatId);

        var chatOwner = await GetChatOwnerByChatId(chat.Id);
        if (chatOwner.UserId != userId)
            throw new OwnershipException("You are not chat Owner");
        
        var updatedChat = await ChangeChat(chat.Id, chatPatchRequestDto);
        updatedChat.UserCount = chat.ChatMembers.Count;

        return updatedChat;
    }

    public async Task<ChatResponseDto> DeleteChat(uint chatId, uint userId)
    {
        var chat = await GetChatById(chatId);
        
        var chatOwner = await GetChatOwnerByChatId(chat.Id);
        if (userId != chatOwner.UserId)
            throw new OwnershipException($"User (ID: {userId}) are not owner of chat (ID: {chat.Id})");
       
        await _chatRepository.DeleteById(chat.Id);
        await _chatRepository.SaveAsync();

        var chatResponseDto = _mapper.Map<ChatResponseDto>(chat);
        chatResponseDto.UserCount = chat.ChatMembers.Count;
        
        return chatResponseDto;
    }

    public async Task<ChatMemberResponseDto> AddChatMember(uint userId, uint chatId, uint userToAddId)
    {
        var chat = await GetChatById(chatId);
        var isUserChatMember = await IsUserChatMember(chat.Id, userId);
        if (!isUserChatMember) 
            throw new AccessDeniedException($"User (ID: {userId}) isn't a member of chat (ID: {chatId})");
        
        var isNewMemberAlreadyInChat = await IsUserChatMember(chat.Id, userToAddId); 
        if (isNewMemberAlreadyInChat)
            throw new DuplicateEntryException($"User (ID: {userId}) is already in chat (ID: {chatId})");
        
        var newChatMember = new ChatMember
        {
            ChatId = chatId,
            CreatedAt = DateTime.Now,
            TypeId = ChatMemberType.Member,
            UserId = userToAddId
        };

        var addedChatMember = await _chatMemberRepository.AddAsync(newChatMember);
        await _chatMemberRepository.SaveAsync();

        return _mapper.Map<ChatMemberResponseDto>(addedChatMember);
    }

    public async Task<List<ChatMemberResponseDto>> GetChatMembers(uint userId, uint chatId, int limit, int nextCursor)
    {
        var chat = await GetChatById(chatId);
        
        var isUserChatMember = await IsUserChatMember(chat.Id, userId);
        if (!isUserChatMember) 
            throw new AccessDeniedException($"User (ID: {userId}) isn't a member of chat (ID: {chatId})");
   
        return chat.ChatMembers.OrderBy(cm => cm.Id)
            .Skip(nextCursor).Take(limit)
            .Select(cm => _mapper.Map<ChatMemberResponseDto>(cm))
            .ToList(); 
    }

    public async Task<ChatMemberResponseDto> UpdateChatMember(uint chatId, uint userId, uint memberId,
        ChatMemberPutDto changeChatMemberRequestDto)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat == null)
            throw new NotFoundException("No community with this Id.");
        
        var chatMemberToChange = await GetChatMember(chatId, memberId);
        if (chatMemberToChange == null)
            throw new NotFoundException("User is not a member of this chat.");

        var chatMember = await GetChatMember(chatId, userId);
        if (chatMember == null)
            throw new OwnershipException("Only community members can change members in communities.");

        switch (chatMember.TypeId)
        {
            case ChatMemberType.Owner when memberId == userId:
                throw new OwnershipException("Owner cant change himself.");
            case ChatMemberType.Admin when memberId != userId && changeChatMemberRequestDto.Type != ChatMemberType.Member:
                throw new OwnershipException("Admin can only change himself to user.");
            case ChatMemberType.Member:
                throw new OwnershipException("Member can't change anything.");
        }

        chatMemberToChange.UpdatedAt = DateTime.Now;
        chatMemberToChange.TypeId = changeChatMemberRequestDto.Type;
        
        _chatMemberRepository.Update(chatMemberToChange);
        await _chatMemberRepository.SaveAsync();
        
        return _mapper.Map<ChatMemberResponseDto>(chatMemberToChange);
    }
    
    public async Task<ChatMemberResponseDto> DeleteChatMember(uint userId, uint userToDeleteId, uint chatId)
    {
        var chat = await GetChatById(chatId);
        
        var chatMemberToDelete = await GetChatMember(chat.Id, userToDeleteId);
        if (chatMemberToDelete == null)
            throw new NotFoundException("User is not a chat member.");

        var chatMember = await GetChatMember(chat.Id, userId);
        if (chatMember == null)
            throw new OwnershipException("Only chat members can delete members from communities.");

        if (userToDeleteId != userId)
        {
            switch (chatMember.TypeId)
            {
                case ChatMemberType.Member:
                    throw new OwnershipException("Chat members can't delete chat members.");
                case ChatMemberType.Admin when chatMemberToDelete.TypeId == ChatMemberType.Admin:
                    throw new OwnershipException("Chat admin can't delete chat admin.");
                case ChatMemberType.Admin when chatMemberToDelete.TypeId == ChatMemberType.Owner:
                    throw new OwnershipException("Chat admin can't delete chat owner.");
            }
        }
        else if (chatMemberToDelete.TypeId == ChatMemberType.Owner)
        {
            throw new OwnershipException("Owner can't delete himself.");
        }

        _chatMemberRepository.Delete(chatMemberToDelete);
        await _chatMemberRepository.SaveAsync();
        return _mapper.Map<ChatMemberResponseDto>(chatMemberToDelete);
    }

    public async Task<List<MessageResponseDto>> GetChatMessages(uint chatId, uint userId, int limit, int nextCursor)
    {
        var chat = await GetChatById(chatId);
        if (chat == null) 
            throw new NotFoundException("Chat with request Id doesn't exist");
        
        var isUserChatMember = await IsUserChatMember(chatId, userId);
        if (!isUserChatMember) 
            throw new AccessDeniedException("User isn't chat member");
        
        return chat.Messages.OrderBy(m => m.Id)
            .Skip(nextCursor).Take(limit)
            .Select(m => _mapper.Map<MessageResponseDto>(m))
            .ToList();
    }

    #region Private Methods
    
    private async Task<Chat> GetChatById(uint chatId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat == null) throw new NotFoundException($"Chat (ID: {chatId}) doesn't exist");
        return chat;
    }

    private async Task<ChatMember> GetChatOwnerByChatId(uint chatId)
    {
        var chatMember = await _chatMemberRepository.GetAsync(
            cm => cm.ChatId == chatId && cm.TypeId == ChatMemberType.Owner);
        if (chatMember == null)
            throw new NotFoundException($"Chat owner doesn't exist in chat (ID: {chatId})");

        return chatMember;
    }
    
    private async Task<ChatMember> GetChatMember(uint chatId, uint userId)
    {
        var chatMember = await _chatMemberRepository.GetAsync(m => m.UserId == userId && m.ChatId == chatId);
        if (chatMember == null) 
            throw new NotFoundException($"User (ID: {userId}) doesn't exist in chat (ID: {chatId})");
        return chatMember;
    }

    private async Task<bool> IsUserChatMember(uint chatId, uint userId)
    {
        var chatMember = await _chatMemberRepository.GetAsync(m => m.UserId == userId && m.ChatId == chatId);
        return chatMember != null;
    }

    private async Task<ChatResponseDto> ChangeChat(uint chatId, ChatPatchDto chatPatchRequestDto)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat == null)
            throw new NotFoundException($"Chat (ID: {chatId}) doesn't exist");
        
        var updated = false;
        if (chatPatchRequestDto.ChatPictureId != null)
        {   
            var media = await _mediaRepository.GetByIdAsync((uint)chatPatchRequestDto.ChatPictureId);
            if (media == null)
                throw new ArgumentException($"Media with id equal {chatPatchRequestDto.ChatPictureId} doesn't exist.");
            
            if (chat.ChatPictureId != chatPatchRequestDto.ChatPictureId)
            {
                chat.ChatPictureId = chatPatchRequestDto.ChatPictureId;
                updated = true;
            }
        }
        if (chatPatchRequestDto.Name != null && chat.Name != chatPatchRequestDto.Name)
        {            
            chat.Name = chatPatchRequestDto.Name;
            updated = true;            
        }
        
        if (updated)
        {
            chat.UpdatedAt = DateTime.Now;
            _chatRepository.Update(chat);
            await _chatRepository.SaveAsync();
        }

        var chatResponseDto = _mapper.Map<ChatResponseDto>(chat);
        chatResponseDto.UserCount = chat.ChatMembers.Count;
        
        return chatResponseDto;
    }

    #endregion
}