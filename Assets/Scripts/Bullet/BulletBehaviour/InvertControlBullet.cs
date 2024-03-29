﻿using UnityEngine;
using System.Collections;

public class InvertControlBullet : MonoBehaviour {

    public float effectDuration = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateInvertEffect(effectDuration);
    }
}
