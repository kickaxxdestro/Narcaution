using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DialogueSystemScript))]
public class DialogueSystemEditorScript : Editor {

	int allUsersAmount = 0;

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

		//get the target
		DialogueSystemScript dial = (DialogueSystemScript)target;

		//get size of array in real time
		dial.dialogueAmount = dial.dialogueList.Length;
		//get all the users that will appear in this dialogue
		allUsersAmount = dial.allUsers.Length;

		//store all the names of the users into an array, to be used in dropdown list
		string[] userChoices = new string[allUsersAmount];
		for(int i = 0; i < userChoices.Length; ++i)
		{
			userChoices[i] = dial.allUsers[i].userName;
		}

		EditorGUILayout.LabelField("DIALOGUE SYSTEM CUSTOM EDITOR", EditorStyles.boldLabel);
		Spaces(2);
		EditorGUILayout.LabelField("Users that will appear in this dialogue", EditorStyles.boldLabel);
		//list all the users in the chat
		for(int i = 0; i < dial.allUsers.Length; ++i)
		{
			dial.allUsers[i] = (DialogueUserScript)EditorGUILayout.ObjectField("User " + (i + 1), dial.allUsers[i], typeof(DialogueUserScript), false);
        }
		//buttons to handle the amount of user
		if(GUILayout.Button("Add New User"))
		{
			//add a new user, dynamically scale the array size
			allUsersAmount += 1;
			System.Array.Resize(ref dial.allUsers, allUsersAmount);
			DialogueUserScript u = new DialogueUserScript();
			dial.allUsers[allUsersAmount - 1] = u;
		}
		else if(GUILayout.Button("Remove Last User"))
		{
			//delete the final user, then decrease the size of the array
			allUsersAmount -= 1;
			dial.allUsers[allUsersAmount] = null;
			System.Array.Resize(ref dial.allUsers, allUsersAmount);
		}

		Spaces (2);
		EditorGUILayout.LabelField("Dialogue Chat", EditorStyles.boldLabel);
		for(int i = 0; i < dial.dialogueList.Length; ++i)
		{
			dial.dialogueList[i].userChoice = EditorGUILayout.Popup(dial.dialogueList[i].userChoice, userChoices);
			dial.dialogueList[i].user = dial.allUsers[dial.dialogueList[i].userChoice];
			if(dial.dialogueList[i].user.userName == "Stop")
			{
				EditorGUILayout.LabelField("Message", "-Disable the Dialogue System-", "TextField");
			}
			else
			{
				dial.dialogueList[i].message = EditorGUILayout.TextField("Message", dial.dialogueList[i].message, GUILayout.MinHeight(80), GUILayout.MaxHeight(20));
			}

			Spaces (2);
		}
		//buttons to handle the dialogues
		if(GUILayout.Button("Add New Dialogue"))
		{
			//add a new user, dynamically scale the array size
			dial.dialogueAmount += 1;
			System.Array.Resize(ref dial.dialogueList, dial.dialogueAmount);
			UserDialogue u = new UserDialogue();
			dial.dialogueList[dial.dialogueAmount - 1] = u;
		}
		else if(GUILayout.Button("Remove Last Dialogue"))
		{
			//delete the final user, then decrease the size of the array
			dial.dialogueAmount -= 1;
			dial.dialogueList[dial.dialogueAmount] = null;
			System.Array.Resize(ref dial.dialogueList, dial.dialogueAmount);
		}


		//sync with original inspector
		if(GUI.changed)
		{
			EditorUtility.SetDirty(dial);
		}
	}
}
