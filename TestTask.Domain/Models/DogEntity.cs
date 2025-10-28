namespace TestTask.Domain.Models
{
    public class DogEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int TailLength { get; set; }
        public int Weight { get; set; }

        public DogEntity(string? name, string? color, int tailLength, int weight)
        { 
            Name = name;
            Color = color;
            TailLength = tailLength;
            Weight = weight;
        }
    }
}
