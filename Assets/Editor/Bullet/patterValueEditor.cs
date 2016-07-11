using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(patternList))]
public class patterValueEditor : Editor {

	bool [] repeat = new bool[100];

	public override void OnInspectorGUI()
	{	
		//get the target
		patternList emb = (patternList)target;

		EditorGUILayout.LabelField("Bullet Pattern Custom Editor", EditorStyles.boldLabel);

		//Run through pattern list
		for (int i=0; i < emb.ListOfPatterns.Count; i++) {
			EditorGUILayout.LabelField("Bullet Pattern Values/ Type", EditorStyles.whiteBoldLabel);

			emb.ListOfPatterns[i].pattern = (patternValues.bulletPattern) EditorGUILayout.EnumPopup("Bullet Pattern:", emb.ListOfPatterns[i].pattern);

			switch (emb.ListOfPatterns[i].pattern) {
			case patternValues.bulletPattern.cone :
				emb.ListOfPatterns[i].coneAngle = EditorGUILayout.IntField("Angle of cone", emb.ListOfPatterns[i].coneAngle);
				goto case patternValues.bulletPattern.circle;

			case patternValues.bulletPattern.circle :
				emb.ListOfPatterns[i].column = EditorGUILayout.IntField("Amount of columns", emb.ListOfPatterns[i].column);
				goto case patternValues.bulletPattern.downward;

			case patternValues.bulletPattern.downward :
			case patternValues.bulletPattern.guided :
				emb.ListOfPatterns[i].columnBullets = EditorGUILayout.IntField("Amount of bullets/column", emb.ListOfPatterns[i].columnBullets);
				emb.ListOfPatterns[i].attackSet = EditorGUILayout.IntField("Attack Pattern Set", emb.ListOfPatterns[i].attackSet);

				EditorGUILayout.LabelField("Intervals/ Timings", EditorStyles.whiteBoldLabel);

				emb.ListOfPatterns[i].bulletInterval = EditorGUILayout.FloatField("Time b/w ea. bullet", emb.ListOfPatterns[i].bulletInterval);
				emb.ListOfPatterns[i].patternInterval = EditorGUILayout.FloatField("Time b/w ea. pattern", emb.ListOfPatterns[i].patternInterval);
				//Delay of bullet spawning
				emb.ListOfPatterns[i].delay = EditorGUILayout.FloatField("Delay of bullet spawning", emb.ListOfPatterns[i].delay);

				EditorGUILayout.LabelField("Repeat Spawning Cycle", EditorStyles.whiteBoldLabel);

				repeat[i] = EditorGUILayout.Toggle("Repeat (Locks in current position)", repeat[i]);

				if(repeat[i]) {
				emb.ListOfPatterns[i].repeatInfinite = EditorGUILayout.Toggle("Repeat infinitely", emb.ListOfPatterns[i].repeatInfinite);
				emb.ListOfPatterns[i].repeatInterval = EditorGUILayout.FloatField("Interval b/w cycles", emb.ListOfPatterns[i].repeatInterval);

				if(!emb.ListOfPatterns[i].repeatInfinite)
					emb.ListOfPatterns[i].repeat = EditorGUILayout.IntField("Amount of cycles", emb.ListOfPatterns[i].repeat);
				}

				break;
			}

			emb.ListOfPatterns[i].moveSpawner = EditorGUILayout.Toggle("Move Spawner for (t)", emb.ListOfPatterns[i].moveSpawner);

			if(emb.ListOfPatterns[i].moveSpawner) {
				emb.ListOfPatterns[i].spawnerMoveInterval = EditorGUILayout.FloatField("Spawner moves for (t)", emb.ListOfPatterns[i].spawnerMoveInterval);
			}

			EditorGUILayout.Space();
			//button to remove pattern
			if(GUILayout.Button("Remove Pattern"))
			{
				emb.ListOfPatterns.Remove(emb.ListOfPatterns[i]);
			}
		}

		emb.spawnerPrefab = (GameObject) EditorGUILayout.ObjectField("Bullet Spawner Prefab", emb.spawnerPrefab, typeof (GameObject), false);

		EditorGUILayout.Space();
		if(GUILayout.Button("Add Pattern"))
		{
			emb.ListOfPatterns.Add(new patternValues());
		}

		//sync with original inspector
		if(GUI.changed)
		{
			EditorUtility.SetDirty(emb);
		}


	}
}
