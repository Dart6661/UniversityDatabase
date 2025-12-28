namespace Core.ModelsDto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public CuratorDto Curator { get; set; } = null!;
        public List<StudentDto> Students { get; set; } = [];
    }
}
