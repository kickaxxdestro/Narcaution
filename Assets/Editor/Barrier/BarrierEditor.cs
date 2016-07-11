using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Barrier), true)]
public class BarrierEditor : Editor 
{
    Barrier barrier;

    void Spaces(int number)
    {
        for (int i = 0; i < number; ++i)
        {
            EditorGUILayout.Space();
        }
    }

    void OnEnable()
    {
        //Get the target
        barrier = (Barrier)target;
        if (barrier.LevelXRegeneration == null)
            barrier.LevelXRegeneration = new float[Barrier.maxBarrierLevel];
        if (barrier.LevelXDurability == null)
            barrier.LevelXDurability = new float[Barrier.maxBarrierLevel];
        if (barrier.LevelXCost == null)
            barrier.LevelXCost = new int[Barrier.maxBarrierLevel];
        if(barrier.LevelXReflectedDamage == null)
            barrier.LevelXReflectedDamage = new float[Barrier.maxBarrierLevel];
    }

    public override void OnInspectorGUI()
    {
        GUI.skin.button.wordWrap = true;

        EditorGUILayout.LabelField("Barrier Editor", EditorStyles.boldLabel);

        barrier.name = EditorGUILayout.TextField("Name", barrier.name);
        barrier.ID = EditorGUILayout.IntField("ID", barrier.ID);
        barrier.icon = (Sprite)EditorGUILayout.ObjectField("Icon", barrier.icon, typeof(Sprite), false);
        barrier.barrierDescription = EditorGUILayout.TextField("Description", barrier.barrierDescription, GUILayout.Height(50));
        barrier.ability = (Barrier.BARRIER_ABILITY)EditorGUILayout.EnumPopup("Ability", barrier.ability);
        barrier.minimumActivationHealth = EditorGUILayout.FloatField("Min. Activation Health", barrier.minimumActivationHealth);
        barrier.size = EditorGUILayout.FloatField("Barrier size", barrier.size);
        barrier.reflectedSpeed = EditorGUILayout.FloatField("Reflected speed", barrier.reflectedSpeed);

        barrier.onAudio = (AudioClip)EditorGUILayout.ObjectField("On Audio", barrier.onAudio, typeof(AudioClip), false);
        barrier.offAudio = (AudioClip)EditorGUILayout.ObjectField("Off Audio", barrier.offAudio, typeof(AudioClip), false);

        Spaces(1);
        EditorGUILayout.LabelField("Upgrade cost");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Barrier.maxBarrierLevel; ++i)
        {
            barrier.LevelXCost[i] = EditorGUILayout.IntField("Level " + (i + 1), barrier.LevelXCost[i]);
        }
        Spaces(1);

        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("Reflected damage");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Barrier.maxBarrierLevel; ++i)
        {
            barrier.LevelXReflectedDamage[i] = EditorGUILayout.FloatField("Level " + (i + 1), barrier.LevelXReflectedDamage[i]);
        }
        Spaces(1);

        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("Regeneration speed");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Barrier.maxBarrierLevel; ++i)
        {
            barrier.LevelXRegeneration[i] = EditorGUILayout.FloatField("Level " + (i + 1), barrier.LevelXRegeneration[i]);
        }
        Spaces(1);

        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("Durability");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Barrier.maxBarrierLevel; ++i)
        {
            barrier.LevelXDurability[i] = EditorGUILayout.FloatField("Level " + (i + 1), barrier.LevelXDurability[i]);
        }
        Spaces(1);


        //sync with original inspector
        if (GUI.changed)
        {
            EditorUtility.SetDirty(barrier);
        }
    }

}
