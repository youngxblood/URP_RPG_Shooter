using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStartTrigger : MonoBehaviour
{
    private CombatMonitor combatMonitor;

    private void Awake() 
    {
        combatMonitor = transform.root.GetComponent<CombatMonitor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                combatMonitor.triggerIsActive = true;
                combatMonitor.ReadyToStartWave = true;
            }
    }
}
