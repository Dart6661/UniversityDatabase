using Core.ModelsDto;
using DatabaseModels.UnivModels;

namespace Core.Serializers
{
    public static class StudentSerializer
    {
        public static StudentDto Serialize(Student student) => new()
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            GroupId = student.GroupId
        };

        public static Student Deserialize(StudentDto studentDto) => new() 
        { 
            Id = studentDto.Id, 
            Name = studentDto.Name, 
            Age = studentDto.Age, 
            GroupId = studentDto.GroupId 
        };
    }
}
