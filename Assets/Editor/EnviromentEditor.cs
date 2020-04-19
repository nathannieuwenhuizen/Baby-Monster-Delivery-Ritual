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
        if (GUILayout.Button("Place Rock (will delete the previous rocks!)"))
        {
            if (myScript != null)
            {
                myScript.PlaceRocks();
            }
        }
        myScript.UpdateSize();

    }

}
