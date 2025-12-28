using Core.ModelsDto;
using DatabaseModels.UnivModels;

namespace Core.Serializers
{
    public static class GroupSerializer
    {
        public static GroupDto Serialize(Group group) => new()
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Curator = CuratorSerializer.Serialize(group.Curator),
            Students = [.. group.Students.Select(student => StudentSerializer.Serialize(student))]
        };

        public static Group Deserialize(GroupDto groupDto) => new() 
        {
            Id = groupDto.Id,
            Name = groupDto.Name,  
            CreationDate = groupDto.CreationDate,
            Curator = CuratorSerializer.Deserialize(groupDto.Curator),
            Students = [.. groupDto.Students.Select(student => StudentSerializer.Deserialize(student))]
        };
    }
}
