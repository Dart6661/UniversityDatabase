namespace Core.ModelsDto
{
    public class CuratorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int GroupId { get; set; }
    }
}
