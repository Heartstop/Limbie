using MoonSharp.Interpreter;
using UnityEngine;

namespace Limbie.Control.Shared.In
{
    [MoonSharpUserData]
    public class Body
    {
        public float Rotation { get; }
        public float Elevation { get; }
        public Body(GameObject body)
        {
            Rotation = body.transform.localRotation.eulerAngles.x;
            Elevation = body.transform.position.y;
        }
    }
}
