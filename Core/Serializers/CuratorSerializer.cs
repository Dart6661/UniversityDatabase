using Core.ModelsDto;
using DatabaseModels.UnivModels;

namespace Core.Serializers
{
    public static class CuratorSerializer
    {
        public static CuratorDto Serialize(Curator curator) => new()
        {
            Id = curator.Id,
            Name = curator.Name,
            Email = curator.Email,
            GroupId = curator.GroupId
        };

        public static Curator Deserialize(CuratorDto curatorDto) => new()
        { 
            Id = curatorDto.Id, 
            Name = curatorDto.Name, 
            Email = curatorDto.Email, 
            GroupId = curatorDto.GroupId 
        };
    }
}
