using UnityEngine;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;

public class Programmable : MonoBehaviour
{
    private Script script;
    private Table state;

    public string Code { get; set; } = string.Empty;

    void Start()
    {
        RegisterUserData();

        script = new Script(CoreModules.Preset_HardSandbox);
        state = new Table(script);
    }

    private void RegisterUserData()
    {
        UserData.RegisterType<Time>();
    }

    void Update()
    {
        SetGlobals();
        Execute();
    }

    private Commands Execute()
    {
        try
        {
            DynValue result = script.DoString(Code, state);
            return result.ToObject<Commands>();
        } catch (Exception e)
        {
            return new Commands { Messages = new string[] { e.ToString() } };
        }
    }

    private void SetGlobals()
    {
        state["time"] = typeof(Time);
    }

    class Commands
    {
        public IList<string> Messages { get; set; }
    }
}
