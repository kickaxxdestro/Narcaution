using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyMovementBehaviour))]
public class EnemyMovementEditorScript : Editor {	
	void Spaces(int number)
	{
		for(int i = 0; i < number; ++i)
		{
			EditorGUILayout.Space();
		}
	}
	
	#region HANDLES_DEBUG
	void OnSceneGUI()
	{
		//mainly for writing text labels on the scene
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.MiddleLeft;
		style.fontSize = 11;
		EnemyMovementBehaviour emb = (EnemyMovementBehaviour)target;
		Vector3 currentPos = emb.transform.position;
		Vector3 targetPos;
		
		//text for the starting position
		Handles.BeginGUI();
		
		for(int i = 0; i < emb.movePoints.Length; ++i)
		{
			targetPos = currentPos + emb.movePoints[i].translateValue;
			Handles.Label(targetPos - emb.transform.right - emb.transform.up * 0.2f, "Element " + (i + 1) , style);
			Handles.Label(targetPos - emb.transform.right - emb.transform.up * 0.5f, "Position: (" + targetPos.x + ", " + targetPos.y + ")", style);
			Handles.Label(targetPos - emb.transform.right - emb.transform.up * 0.8f, "Speed: " + emb.movePoints[i].translateSpeed, style);
			Handles.Label(targetPos - emb.transform.right - emb.transform.up * 1.1f, "Wait Time: " + emb.movePoints[i].waitTime, style);
			if(emb.movePoints[i].isLerp == true)
			{
				Handles.Label(targetPos - emb.transform.right - emb.transform.up * 1.4f, "Acceleration: " + emb.movePoints[i].isLerp, style);
			}
			if(emb.movePoints[i].isShooting == true)
			{
				Handles.Label(targetPos - emb.transform.right - emb.transform.up * 1.7f, "Will Shoot: " + emb.movePoints[i].isShooting, style);
				if(emb.movePoints[i].shootAtPlayer == true)
				{
					Handles.Label(targetPos - emb.transform.right - emb.transform.up * 2.0f, "Shoot Player: " + emb.movePoints[i].shootAtPlayer, style);
				}
			}
			currentPos = targetPos;
		}
		
		Handles.EndGUI();
	}
	#endregion
				
	public override void OnInspectorGUI()
	{
		//draws the default inspector
		//DrawDefaultInspector();
		
		//get the target
		EnemyMovementBehaviour emb = (EnemyMovementBehaviour)target;
		
		//get size of array in real time
		emb.moveAmount = emb.movePoints.Length;
		
		EditorGUILayout.LabelField("ENEMY MOVEMENT BEHAVIOUR CUSTOM EDITOR", EditorStyles.boldLabel);
		
		emb.isLooping = EditorGUILayout.Toggle("Loop this Behaviour", emb.isLooping);
		emb.loopInfinite = EditorGUILayout.Toggle("Loop this Behaviour Forever", emb.loopInfinite);
		emb.moveAndShoot = EditorGUILayout.Toggle("Shoot while moving", emb.moveAndShoot);

		//ARRAY
		SerializedProperty array = serializedObject.FindProperty ("shootingPoints");
		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.PropertyField (array, true);
		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		EditorGUIUtility.LookLikeControls();

		if(emb.isLooping == true)
		{
			EditorGUI.indentLevel = 1;
			emb.loopAmt = EditorGUILayout.IntField("Loop Amount", emb.loopAmt);
			EditorGUI.indentLevel = 0;
		}
		emb.shootRate = EditorGUILayout.FloatField("Shoot Rate", emb.shootRate);

		emb.bulletType = (EnemyMovementBehaviour.BulletType)EditorGUILayout.EnumPopup("Bullet Type", emb.bulletType);
		Spaces(1);
		EditorGUILayout.LabelField("Total Translate Elements: " + emb.movePoints.Length, EditorStyles.boldLabel);
		Spaces(1);
		for(int i = 0; i < emb.movePoints.Length; ++i)
		{
			EditorGUI.indentLevel = 1;
			EditorGUILayout.LabelField("Translate Element " + (i + 1).ToString());
			EditorGUI.indentLevel = 2;
			emb.movePoints[i].translateValue = EditorGUILayout.Vector2Field("Value", emb.movePoints[i].translateValue);
			emb.movePoints[i].translateSpeed = EditorGUILayout.FloatField("Speed", emb.movePoints[i].translateSpeed);
			emb.movePoints[i].waitTime = EditorGUILayout.FloatField("Waiting Time", emb.movePoints[i].waitTime);
			emb.movePoints[i].isLerp = EditorGUILayout.Toggle("Acceleration", emb.movePoints[i].isLerp);
			emb.movePoints[i].isShooting = EditorGUILayout.Toggle("Will Shoot", emb.movePoints[i].isShooting);
			if(emb.movePoints[i].isShooting == true)
			{
				EditorGUI.indentLevel = 3;
				emb.movePoints[i].shootAtPlayer = EditorGUILayout.Toggle("Shoot Player", emb.movePoints[i].shootAtPlayer);
			}
			EditorGUI.indentLevel = 1;
			Spaces(2);
		}
		
		//buttons to handle the translate element list
		if(GUILayout.Button("Add New Translate Element"))
		{
			//increase the size of the array then add the new object at the end
			emb.moveAmount += 1;
			System.Array.Resize(ref emb.movePoints, emb.moveAmount);
			PathNodes p = new PathNodes();
			emb.movePoints[emb.moveAmount - 1] = p;
		}
		else if(GUILayout.Button("Remove Last Element"))
		{
			//delete the final element then decrease the size of the array
			emb.moveAmount -= 1;
			emb.movePoints[emb.moveAmount] = null;
			System.Array.Resize(ref emb.movePoints, emb.moveAmount);
		}
		
		//sync with original inspector
		if(GUI.changed)
		{
			EditorUtility.SetDirty(emb);
		}
	}
}