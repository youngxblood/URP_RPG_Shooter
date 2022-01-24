using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStartTrigger : MonoBehaviour
{
    private CombatMonitor combatMonitor;
    public bool hasBeenTriggered = false;

    private void Awake() 
    {
        combatMonitor = transform.root.GetComponent<CombatMonitor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !hasBeenTriggered)
            {
                hasBeenTriggered = true;
                combatMonitor.triggerIsActive = true;
                combatMonitor.ReadyToStartWave = true;
            }
    }
}
