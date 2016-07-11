using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TransitionListHandler))]
public class TransitionListHandlerEditor : Editor
{
    int tempIndex = 0;
    int removeIndex = 0;
    void Spaces(int number)
    {
        for (int i = 0; i < number; ++i)
        {
            EditorGUILayout.Space();
        }
    }

    public override void OnInspectorGUI()
    {
        //draws the default inspector gui
        //DrawDefaultInspector();

        //get the target
        TransitionListHandler list = (TransitionListHandler)target;

        //get the size of array in real time
        //lvl.patternAmount = lvl.patternLists.Length;
        EditorGUILayout.LabelField("Transition List Handler Editor", EditorStyles.boldLabel);
        Spaces(2);
        list.ListID = EditorGUILayout.IntField("Transition List ID", list.ListID);

        Spaces(1);

        if (list.transitionList != null)
            for (int i = 0; i < list.transitionList.Count; ++i)
            {
                EditorGUI.indentLevel = 1;
                EditorGUILayout.LabelField("Transition " + (i + 1).ToString());
                EditorGUI.indentLevel = 2;
                list.transitionList[i].transitionType = (Transition.TransitionType)EditorGUILayout.EnumPopup("Transition Type", list.transitionList[i].transitionType);

                list.transitionList[i].doWithPrevious = EditorGUILayout.Toggle("Do with previous", list.transitionList[i].doWithPrevious);

                if (list.transitionList[i].transitionType != Transition.TransitionType.TRANSITION_WAIT)
                    list.transitionList[i].targetObject = (GameObject)EditorGUILayout.ObjectField("Target object", list.transitionList[i].targetObject, typeof(GameObject), true);
                
                switch(list.transitionList[i].transitionType)
                {
                    case Transition.TransitionType.TRANSITION_FADE_IN_TEXT:
                    case Transition.TransitionType.TRANSITION_FADE_OUT_TEXT:
                    case Transition.TransitionType.TRANSITION_FADE_IN_IMAGE:
                    case Transition.TransitionType.TRANSITION_FADE_OUT_IMAGE:
                        list.transitionList[i].transitionTime = EditorGUILayout.FloatField("Fade time", list.transitionList[i].transitionTime);
                        list.transitionList[i].fadeAmount = EditorGUILayout.FloatField("Fade amount", list.transitionList[i].fadeAmount);
                        break;
                    case Transition.TransitionType.TRANSITION_TRANSLATE:
                        list.transitionList[i].transitionTime = EditorGUILayout.FloatField("Move time", list.transitionList[i].transitionTime);
                        list.transitionList[i].transformAmount = EditorGUILayout.Vector3Field("Translation Amount", list.transitionList[i].transformAmount);
                        break;
                    case Transition.TransitionType.TRANSITION_SCALE:
                        list.transitionList[i].transitionTime = EditorGUILayout.FloatField("Scaling time", list.transitionList[i].transitionTime);
                        list.transitionList[i].transformAmount = EditorGUILayout.Vector3Field("Scaling Amount", list.transitionList[i].transformAmount);
                        break;
                    case Transition.TransitionType.TRANSITION_ROTATE:
                        list.transitionList[i].transitionTime = EditorGUILayout.FloatField("Rotation time", list.transitionList[i].transitionTime);
                        list.transitionList[i].transformAmount = EditorGUILayout.Vector3Field("Rotation Amount", list.transitionList[i].transformAmount);
                        break;
                    case Transition.TransitionType.TRANSITION_WAIT:
                        list.transitionList[i].transitionTime = EditorGUILayout.FloatField("Wait time", list.transitionList[i].transitionTime);
                        break;
                }
                EditorGUI.indentLevel = 2;
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Move up", GUILayout.Width(80), GUILayout.Height(20)))
                {
                    if(i != 0)
                    {
                        Transition temp = list.transitionList[i];
                        list.transitionList[i] = list.transitionList[i - 1];
                        list.transitionList[i - 1] = temp;
                    }
                }
                else if (GUILayout.Button("Move down", GUILayout.Width(80), GUILayout.Height(20)))
                {
                    if(i != list.transitionList.Count - 1)
                    {
                        Transition temp = list.transitionList[i];
                        list.transitionList[i] = list.transitionList[i + 1];
                        list.transitionList[i + 1] = temp;
                    }
                }
                EditorGUILayout.EndHorizontal();
                Spaces(2);
            }

        //buttons to handle the patterns list
        if (GUILayout.Button("Add Transition"))
        {
            list.transitionList.Add(new Transition());
        }
        else if (GUILayout.Button("Remove Last Transition"))
        {
            list.transitionList.RemoveAt(list.transitionList.Count - 1);
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add last transition to index", GUILayout.Width(180), GUILayout.Height(20)))
        {
            if (tempIndex != list.transitionList.Count - 1)
            {
                list.transitionList.Insert(tempIndex, list.transitionList[list.transitionList.Count - 1]);
                list.transitionList.RemoveAt(list.transitionList.Count - 1);
            }
        }
        EditorGUIUtility.labelWidth = 140;
        tempIndex = EditorGUILayout.IntField("Index", tempIndex, GUILayout.ExpandWidth(false));
        EditorGUIUtility.labelWidth = 0;
        EditorGUILayout.EndHorizontal();

        //Removes transition at specified number
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove transition", GUILayout.Width(180), GUILayout.Height(20)))
        {
            if (removeIndex > 0 && removeIndex <= list.transitionList.Count)
            {
                list.transitionList.RemoveAt(removeIndex - 1);
            }
        }
        EditorGUIUtility.labelWidth = 140;
        removeIndex = EditorGUILayout.IntField("Transition number", removeIndex, GUILayout.ExpandWidth(false));
        EditorGUIUtility.labelWidth = 0;
        EditorGUILayout.EndHorizontal();

        //sync with original inspector
        if (GUI.changed)
        {
            EditorUtility.SetDirty(list);
        }
    }
}
