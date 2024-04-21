namespace Hyperar.HPA.Shared.Models.UI.Download
{
    public class TaskDetailRow
    {
        public TaskDetailRow(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}