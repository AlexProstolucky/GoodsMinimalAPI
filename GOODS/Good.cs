namespace Goods
{
    public class Good
    {
        public string id;
        public string name;
        public string description;
        public string image_path;

        public Good(string id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        public Good(string id, string name, string description, string image)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.image_path = image;
        }
    }
}
