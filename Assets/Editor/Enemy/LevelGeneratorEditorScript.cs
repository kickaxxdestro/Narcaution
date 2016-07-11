using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelGeneratorScript))]
public class LevelGeneratorEditorScript : Editor {

	void Spaces(int number)
	{
		for(int i = 0; i < number; ++i)
		{
			EditorGUILayout.Space();
		}
	}

	public override void OnInspectorGUI()
	{
		//draws the default inspector gui
		//DrawDefaultInspector();

		//get the target
		LevelGeneratorScript lvl = (LevelGeneratorScript)target;

		//get the size of array in real time
		lvl.patternAmount = lvl.patternLists.Length;
		EditorGUILayout.LabelField("LEVEL GENERATOR CUSTOM EDITOR", EditorStyles.boldLabel);
		Spaces (2);
        lvl.levelID = EditorGUILayout.IntField("Level ID", lvl.levelID);
        lvl.description = EditorGUILayout.TextField("Description", lvl.description);
		lvl.dialogueSystem = (GameObject)EditorGUILayout.ObjectField("Dialogue", lvl.dialogueSystem, typeof(GameObject), false);
        lvl.scoreBonusAmt = EditorGUILayout.IntField("Target score", lvl.scoreBonusAmt);
        EditorGUILayout.LabelField("Total Number of Pattern Objects: " + lvl.patternLists.Length, EditorStyles.boldLabel);
		Spaces (1);

		for(int i = 0; i < lvl.patternLists.Length; ++i)
		{
			EditorGUI.indentLevel = 1;
			EditorGUILayout.LabelField("Pattern Object " + (i + 1).ToString());
			EditorGUI.indentLevel = 2;
			lvl.patternLists[i].waitTime = EditorGUILayout.FloatField("Wait Time", lvl.patternLists[i].waitTime);
			lvl.patternLists[i].enemyPattern = (GameObject)EditorGUILayout.ObjectField("Pattern Object", lvl.patternLists[i].enemyPattern, typeof(GameObject), false);
			EditorGUI.indentLevel = 1;
			Spaces(2);
		}

		//buttons to handle the patterns list
		if(GUILayout.Button("Add Pattern Object"))
		{
			//increase the size of the array then add the new object at the end
			lvl.patternAmount += 1;
			System.Array.Resize(ref lvl.patternLists, lvl.patternAmount);
			PatternList p = new PatternList();
			lvl.patternLists[lvl.patternAmount - 1] = p;
		}
		else if(GUILayout.Button("Remove Last Pattern Object"))
		{
			//delete the final element then decrease the size of the array
			lvl.patternAmount -= 1;
			lvl.patternLists[lvl.patternAmount] = null;
			System.Array.Resize(ref lvl.patternLists, lvl.patternAmount);
		}

		//sync with original inspector
		if(GUI.changed)
		{
			EditorUtility.SetDirty(lvl);
		}
	}

}
