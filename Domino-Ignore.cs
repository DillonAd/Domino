using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace domino
{
    [Command(Name = "ignore")]
    [HelpOption]
    public class Domino_Ignore
    {
        [Argument(0, Name = "pattern")]
        [Required]
        public virtual string Pattern { get; set; }

        private async Task OnExecuteAsync() =>
            await File.AppendAllTextAsync(".dominoignore", Pattern);
    }
}
