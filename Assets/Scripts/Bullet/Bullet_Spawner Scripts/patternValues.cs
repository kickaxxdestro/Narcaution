using UnityEngine;
using System.Collections;

[System.Serializable]
public class patternValues {

	public enum bulletPattern {
		cone,
		circle,
		helix,		
		downward,		//Single Line, straight shot (down)
		guided		//(x) amount of bullets fired in (y) direction (curr player position)
	}

	//Move spawner (For Cluster Bomb Only)
	public bool moveSpawner;
	public bool repeatInfinite;
	//Allow the spawner to start shooting immediately (For Cluster Bomb Only) 
	//public bool startShooting;
	//Enum pattern
	public bulletPattern pattern;
	[Tooltip("Interval between each bullet")]
	public float bulletInterval;
	[Tooltip("Interval bewtween each pattern (Timer for next pattern starts when current pattern is activated)")]
	public float patternInterval;	
	//Duration spawner moves
	public float spawnerMoveInterval;
	//Delay
	public float delay;
	//Duration between each repeat
	public float repeatInterval;
	//public bool shoot;
	public int column, columnBullets, coneAngle, attackSet, repeat;
}
