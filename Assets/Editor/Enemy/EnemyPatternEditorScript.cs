using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyPatternBehaviour))]
public class EnemyPatternEditorScript : Editor {
	
	void Spaces(int number)
	{
		for(int i = 0; i < number; ++i)
		{
			EditorGUILayout.Space();
		}
	}
	
	public override void OnInspectorGUI()
	{
		//draws the default inspector
		//DrawDefaultInspector();
		
		//get the target of the script
		EnemyPatternBehaviour epb = (EnemyPatternBehaviour)target;
		
		//custom editor menu
		EditorGUILayout.LabelField("ENEMY PATTERN BEHAVIOUR CUSTOM EDITOR", EditorStyles.boldLabel);
		epb.enemyObject = (GameObject)EditorGUILayout.ObjectField("Enemy Object", epb.enemyObject, typeof(GameObject), false);
		epb.type = (EnemyPatternBehaviour.PatternTypes)EditorGUILayout.EnumPopup("Pattern Type", epb.type);
		//if the type is single, set the spawn amount to 1
		if(epb.type == EnemyPatternBehaviour.PatternTypes.singleType)
		{
			epb.amountToSpawn = 1;
			EditorGUILayout.LabelField("Amount","1", "TextField"); 
		}
		else
		{
			epb.amountToSpawn = EditorGUILayout.IntField("Amount", epb.amountToSpawn);
		}
		epb.timeInterval = EditorGUILayout.FloatField("Time Interval", epb.timeInterval);
		epb.startOffset = EditorGUILayout.Vector3Field("Offset Start Position", epb.startOffset);
			
		//sync with original inspector
		if(GUI.changed)
		{
			EditorUtility.SetDirty(epb);
		}
	}
}