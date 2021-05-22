using UnityEngine;
using MoonSharp.Interpreter;
using System;
using System.Linq;

namespace Limbie.Control
{
    public class Programmable : MonoBehaviour
    {
        private Script script;
        private Table state;

        public string Code = string.Empty;
        public RobotActor robotActor;
        public float TopMotorSpeed = 10;

        void Start()
        {
            RegisterUserData();

            script = new Script(CoreModules.Preset_HardSandbox);
            state = new Table(script);
        }

        private void RegisterUserData()
        {
            UserData.RegisterType<Time>();
            UserData.RegisterAssembly();
        }

        void Update()
        {
            SetGlobals();
            RemoveFunctions(state);
            var commands = ExecuteCode();
            ExecuteCommands(commands);
        }

        private void ExecuteCommands(Shared.Out.Commands commands)
        {
            ExecuteCommandLimbs(commands);
        }

        private void ExecuteCommandLimbs(Shared.Out.Commands commands)
        {
            var limbs = commands.Limbs;
            var maxMotorSpeed = Mathf.Abs(TopMotorSpeed);
            var minMotorSpeed = -maxMotorSpeed;


            void UpdateHinges(Shared.Out.Limb limb, ref HingeJoint2D hinge)
            {
                var motor = hinge.motor;
                motor.motorSpeed = Mathf.Min(maxMotorSpeed, Mathf.Max(minMotorSpeed, limb.MotorSpeed));
                hinge.motor = motor;
            }

            UpdateHinges(limbs.awayLimb, ref robotActor.awayLimb);
            UpdateHinges(limbs.facingLimb, ref robotActor.facingLimb);
            UpdateHinges(limbs.outerAwayLimb, ref robotActor.outerAwayLimb);
            UpdateHinges(limbs.outerFacingLimb, ref robotActor.outerFacingLimb);
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
            static Shared.Out.Commands makeCommands()
            {
                return new Shared.Out.Commands();
            }
            try
            {
                state["createOut"] = (Func<Shared.Out.Commands>)makeCommands;
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
            state["time"] = typeof(Time);

            if (robotActor != null)
            {
                state["limbs"] = new Shared.In.Limbs(robotActor);
            }
            else
            {
                state.Remove("limbs");
            }
        }
    }
}