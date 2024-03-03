
namespace MudaIpDahora.Models
{
    public class Log
    {
        public string StepName { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return StepName + ": " + Text;
        }
    }
}
