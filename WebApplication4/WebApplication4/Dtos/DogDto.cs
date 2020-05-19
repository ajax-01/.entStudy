using WebApplication4.Modles;

namespace WebApplication4.Dtos
{
    public class DogDto
    {
        public string Name { get; set; }
        public DogTypes Type { get; set; }
        public PersonsDto Person { get; set; }
    }
}