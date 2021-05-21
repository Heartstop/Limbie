using UnityEngine;
using MoonSharp.Interpreter;
using System;
using Limbie.Control.Shared;
using System.Linq;

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
        RemoveFunctions(state);
        Execute();
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

    private Commands Execute()
    {
        try
        {
            DynValue result = script.DoString(Code, state);
            return result.ToObject<Commands>();
        } catch (Exception e)
        {
            return new Commands { Error = e.ToString() };
        }
    }

    private void SetGlobals()
    {
        state["time"] = typeof(Time);
    }
}
