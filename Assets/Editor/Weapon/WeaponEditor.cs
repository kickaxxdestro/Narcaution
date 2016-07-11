using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Weapon), true)]
public class WeaponEditor : Editor
{
    Weapon weapon;

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
        weapon = (Weapon)target;
        if (weapon.LevelXFiringSpeed == null)
            weapon.LevelXFiringSpeed = new float[Weapon.maxWeaponLevel];
        if (weapon.LevelXBulletDamage == null)
            weapon.LevelXBulletDamage = new float[Weapon.maxWeaponLevel];
        if (weapon.LevelXNumberOfProjectiles == null)
            weapon.LevelXNumberOfProjectiles = new int[Weapon.maxWeaponLevel];
        if (weapon.LevelXCost == null)
            weapon.LevelXCost = new int[Weapon.maxWeaponLevel];
    }

    public override void OnInspectorGUI()
    {
        GUI.skin.button.wordWrap = true;

        EditorGUILayout.LabelField("Weapon Editor", EditorStyles.boldLabel);
        
        weapon.name = EditorGUILayout.TextField("Name", weapon.name);
        weapon.ID = EditorGUILayout.IntField("ID", weapon.ID);
        weapon.icon = (Sprite)EditorGUILayout.ObjectField("Icon", weapon.icon, typeof(Sprite), false);
        weapon.weaponDescription = EditorGUILayout.TextField("Description", weapon.weaponDescription, GUILayout.Height(50));
        weapon.firingPattern = (Weapon.FIRING_PATTERN)EditorGUILayout.EnumPopup("Firing Pattern", weapon.firingPattern);
        weapon.projectileMovement = (Weapon.PROJECTILE_MOVEMENT)EditorGUILayout.EnumPopup("Projectile Movement", weapon.projectileMovement);
        weapon.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", weapon.projectileSpeed);

        Spaces(1);
        EditorGUILayout.LabelField("Upgrade cost");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Weapon.maxWeaponLevel; ++i)
        {
            weapon.LevelXCost[i] = EditorGUILayout.IntField("Level " + (i + 1), weapon.LevelXCost[i]);
        }
        Spaces(1);

        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("Firing speed");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Weapon.maxWeaponLevel; ++i)
        {
            weapon.LevelXFiringSpeed[i] = EditorGUILayout.FloatField("Level " + (i + 1), weapon.LevelXFiringSpeed[i]);
        }
        Spaces(1);

        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("Projectile damage");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Weapon.maxWeaponLevel; ++i)
        {
            weapon.LevelXBulletDamage[i] = EditorGUILayout.FloatField("Level " + (i + 1), weapon.LevelXBulletDamage[i]);
        }
        Spaces(1);

        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("Number of projectiles");
        EditorGUI.indentLevel = 1;
        for (int i = 0; i < Weapon.maxWeaponLevel; ++i)
        {
            weapon.LevelXNumberOfProjectiles[i] = EditorGUILayout.IntField("Level " + (i + 1), weapon.LevelXNumberOfProjectiles[i]);
        }
        Spaces(1);

        //sync with original inspector
        if (GUI.changed)
        {
            EditorUtility.SetDirty(weapon);
        }
    }

}
