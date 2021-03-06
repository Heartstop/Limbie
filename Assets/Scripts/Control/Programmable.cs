using UnityEngine;
using MoonSharp.Interpreter;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Text;

namespace Limbie.Control
{
    public class Programmable : MonoBehaviour
    {
        private Script script;
        private Shared.In.Print printer;
        private StringBuilder textOutput;

        [HideInInspector]
        public string Code = string.Empty;
        public RobotActor RobotActor;
        public GameObject RobotBody;
        public float TopMotorSpeed = 10;
        public Text Output;

        private bool Mirrored { get { return RobotActor.transform.localScale.x < 0; } } 

        void Start()
        {
            RegisterUserData();

            textOutput = new StringBuilder();
            printer = new Shared.In.Print(ref textOutput);
            ResetScriptEngine();
        }

        public void ResetScriptEngine()
        {
            script = new Script(CoreModules.Preset_HardSandbox);
            script.Options.UseLuaErrorLocations = false;
            script.Options.DebugPrint = s => textOutput.AppendLine(s);
        }

        private void RegisterUserData()
        {
            UserData.RegisterType<Time>();
            UserData.RegisterAssembly();
        }

        void Update()
        {
            SetGlobals();
            RemoveFunctions(script.Globals);
            var commands = ExecuteCode();
            ExecuteCommands(commands);
        }

        private void ExecuteCommands(Shared.Out.Commands commands)
        {
            ExecuteOutput(commands);
            ExecuteCommandLimbs(commands);
        }

        private void ExecuteOutput(Shared.Out.Commands commands)
        {
            if (Output == null)
                return;

            textOutput.AppendLine(commands.Error);
            var text = textOutput.ToString();
            textOutput.Clear();
            Output.text = text;
        }

        private void ExecuteCommandLimbs(Shared.Out.Commands commands)
        {
            var limbs = commands.Limbs;
            var maxMotorSpeed = Mathf.Abs(TopMotorSpeed);
            var minMotorSpeed = -maxMotorSpeed;

            void UpdateHinges(Shared.Out.Limb limb, ref HingeJoint2D hinge)
            {
                var motor = hinge.motor;
                var speed = Mathf.Min(maxMotorSpeed, Mathf.Max(minMotorSpeed, limb.MotorSpeed));
                motor.motorSpeed = Mirrored ? -speed : speed;
                hinge.motor = motor;

                hinge.useMotor = limb.MotorEnabled;
            }

            UpdateHinges(limbs.Away, ref RobotActor.awayLimb);
            UpdateHinges(limbs.Facing, ref RobotActor.facingLimb);
            UpdateHinges(limbs.OuterAway, ref RobotActor.outerAwayLimb);
            UpdateHinges(limbs.OuterFacing, ref RobotActor.outerFacingLimb);
        }

        private static void RemoveFunctions(Table table)
        {
            var keys = table.Pairs
                .Where(kvp => kvp.Value.Type == DataType.Function)
                .Select(kvp => kvp.Key)
                .ToArray();

            foreach (var key in keys)
                table.Remove(key);
        }

        private Shared.Out.Commands ExecuteCode()
        {
            var state = script.Globals;
            static Shared.Out.Commands makeCommands()
            {
                return new Shared.Out.Commands();
            }
            try
            {
                state["_cmd"] = (Func<Shared.Out.Commands>)makeCommands;
                DynValue result = script.DoString(Code, state);
                return result.ToObject<Shared.Out.Commands>() ?? new Shared.Out.Commands();
            }
            catch (Exception e)
            {
                return new Shared.Out.Commands { Error = e.Message };
            }
        }

        private void SetGlobals()
        {
            const string
                timeGlobal = "_time",
                limbsGlobal = "_limbs",
                bodyGlobal = "_body",
                outGlobal = "_out";

            var state = script.Globals;
            state[timeGlobal] = typeof(Time);
            state[limbsGlobal] = new Shared.In.Limbs(RobotActor, Mirrored);
            state[bodyGlobal] = new Shared.In.Body(RobotBody);
            state[outGlobal] = printer;
        }
    }
}