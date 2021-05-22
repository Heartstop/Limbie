using MoonSharp.Interpreter;
using System.Text;

namespace Limbie.Control.Shared.In
{
    [MoonSharpUserData]
    public class Print
    {
        private readonly StringBuilder output;

        public Print(ref StringBuilder output)
        {
            this.output = output;
        }

        public void WriteLine(string line)
        {
            output.AppendLine(line);
        }

        public void Write(string text)
        {
            output.Append(text);
        }
    }
}
