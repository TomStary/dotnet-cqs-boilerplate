namespace BoilerPlate.Entities.Model
{
    public class Book : BaseEnity
    {
        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
    }
}