using UnityEngine;
using System.Collections;

public class BeamBulletAnimation : MonoBehaviour {

    void OnEnable()
    {
        GetComponent<Animator>().Play(0);
    }
}
