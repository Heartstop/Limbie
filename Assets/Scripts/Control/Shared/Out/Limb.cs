using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.Out
{
    [MoonSharpUserData]
    public class Limb
    {
        public float MotorSpeed { get; set; }
        public bool MotorEnabled { get; set; } = true;
    }
}
