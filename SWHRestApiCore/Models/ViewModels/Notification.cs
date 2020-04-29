namespace SWHRestApiCore.Models.ViewModels
{
    public class Notification
    {
        public string title { get; set; }
        public string text { get; set; }


        public Notification(string title, string text)
        {
            this.title = title;
            this.text = text;
        }
    }
}