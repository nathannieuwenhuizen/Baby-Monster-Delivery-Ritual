using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ScrollBackGround))]
public class EnviromentEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        ScrollBackGround myScript = (ScrollBackGround)target;
        /*if (GUILayout.Button("UpdateSize"))
        {
            if (myScript != null)
            {
                myScript.GenerateColorPallet();
            }
        }*/
        myScript.UpdateSize();

    }

}
