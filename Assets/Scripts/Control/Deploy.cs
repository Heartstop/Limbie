using Limbie.Control;
using System;
using UnityEngine;

public class Deploy : MonoBehaviour
{
    public InGameCodeEditor.CodeEditor CodeEditor;
    public Programmable Programmable;
    public bool constantDeploy = true;

    public static readonly string defaultText = "--[[" + Environment.NewLine +
        "Live program your machine with lua!" + Environment.NewLine
        + "Return a command object to define what action to take. Create a command object with `_out()`." +
        " Modify its properties such as limb movement with `cmd.Limbs.LIMBPART.MotorSpeed`, or disable it altogether by changing `MotorEnabled`." +
        Environment.NewLine + "Parts:" +
        "Facing" + Environment.NewLine +
        "OuterFacing" + Environment.NewLine +
        "Away" + Environment.NewLine +
        "OuterAway" + Environment.NewLine
        + Environment.NewLine + "The output of the script is displayed below. Each frame executes the script and clears the output."
        + Environment.NewLine + "Now remove this placeholder and start programming!"
        + Environment.NewLine + "--]]";

    void Apply()
    {
        Programmable.Code = CodeEditor.Text;
    }

    void Drop()
    {
        CodeEditor.Text = Programmable.Code ?? string.Empty;
    }

    void Start()
    {
        CodeEditor.Text = defaultText;
    }

    void Update()
    {
        if (constantDeploy)
            Apply();
    }
}
