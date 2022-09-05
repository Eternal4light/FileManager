namespace DataBaseHelper.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string DateVisited { get; set; }

        public Visit() { }
        public Visit(string fileName, DateTime dateVisited)
        {
            FileName = fileName;
            DateVisited = dateVisited.ToString();
        }
    }
}
