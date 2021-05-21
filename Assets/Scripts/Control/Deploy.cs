using Limbie.Control;
using UnityEngine;

public class Deploy : MonoBehaviour
{
    public InGameCodeEditor.CodeEditor CodeEditor;
    public Programmable Programmable;
    public bool constantDeploy = true;

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
        
    }

    void Update()
    {
        if (constantDeploy)
            Apply();
    }
}
