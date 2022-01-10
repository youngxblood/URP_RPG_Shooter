using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDecision : AIDecision
{
    [field: SerializeField] [field: Range(0.1f, 10f)] public float Distance { get; set; } = 5f;
    [SerializeField] private bool isDamaged = false;

    private void Awake() 
    {
        
    }

    public override bool MakeADecision()
    {
        if(Vector3.Distance(enemyBrain.Target.transform.position, transform.position) < Distance && isDamaged)
        {
            if (aiActionData.TargetSpotted == false)
            {
                aiActionData.TargetSpotted = true;
            } 
        }
        else
        {
            aiActionData.TargetSpotted = false;
        }
        return aiActionData.TargetSpotted;
    }

    protected void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Distance);
            Gizmos.color = Color.white;
        }
    }
}