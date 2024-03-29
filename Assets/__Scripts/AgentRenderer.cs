using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AgentRenderer : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FaceDirection(Vector2 pointerInput)
    {
        if (!UIController.Instance.gameIsPaused)
        {
            var direction = (Vector3)pointerInput - transform.position; //? Need to investigate this (casting?)
            var result = Vector3.Cross(Vector2.up, direction); //? Need to investigate this

            if (result.z > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (result.z < 0)
            {
                spriteRenderer.flipX = false;
            }
        }

    }
}
