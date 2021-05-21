using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.Out
{
    [MoonSharpUserData]
    public class Commands
    {
        public string Error { get; set; }

        public Limbs Limbs { get; set; } = new Limbs();
    }
}