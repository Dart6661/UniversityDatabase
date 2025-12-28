namespace DatabaseModels.UnivModels
{
    public class Curator
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}
