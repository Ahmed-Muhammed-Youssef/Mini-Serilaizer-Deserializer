namespace MiniSerializerDeserializer.UnitTests.Helpers.Models
{
    public class Student
    {
        public StudentLevel StudentLevel { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<Course>? Courses { get; set; }
    }
    public enum StudentLevel
    {
        Beginner,
        Intermediate,
        Advanced
    }
}
