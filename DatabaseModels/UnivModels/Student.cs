namespace DatabaseModels.UnivModels
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}
