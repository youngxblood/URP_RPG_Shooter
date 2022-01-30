using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    int layerMask = 1 << 7; // Used for getting player on CheckForTargets
    bool trapIsActive = false;
    float timeUntilNextDamage = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !trapIsActive)
        {
            TriggerSpikeTrap();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DisableSpikeTrap();
            trapIsActive = false;
            timeUntilNextDamage = 0;
        }
    }

    private void Update()
    {
        if (trapIsActive)
        {
            if (CheckForTargets() != null)
                HandleTargetDamage(CheckForTargets());
        }
    }

    private void TriggerSpikeTrap()
    {
        StartCoroutine(SpikeTrapTimeToActivate());
    }

    #region Helpers
    private void DisableSpikeTrap()
    {
        animator.SetBool("isActivated", false);
        trapIsActive = false;
    }

    public IEnumerator SpikeTrapTimeToActivate()
    {
        yield return new WaitForSeconds(trapTimeToActivate);
        if (CheckForTargets())
        {
            animator.SetBool("isActivated", true);
            trapAudioPlayer.PlayActivationClip();
            trapIsActive = true;
        }
    }

    private void HandleTargetDamage(Collider2D other)
    {
        if (timeUntilNextDamage > 0)
            timeUntilNextDamage -= Time.deltaTime;

        if (timeUntilNextDamage <= 0)
        {
            var hittable = other.GetComponent<IHittable>();
            hittable?.GetHit(trapDamage, gameObject);
            timeUntilNextDamage = damageDelay;
        }
    }

    private Collider2D CheckForTargets()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1f, layerMask);
        return collider;
    }

    #endregion
}
