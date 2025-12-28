namespace DatabaseModels.UnivModels
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public Curator Curator { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = [];
    }
}
