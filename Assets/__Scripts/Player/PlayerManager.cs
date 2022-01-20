using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int playerLives;
    [SerializeField] private PlayerStats playerStats;
    private Player player;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerStats = player.gameObject.GetComponentInChildren<PlayerStats>();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(PlayerRespawnCoroutine());
    }

    IEnumerator PlayerRespawnCoroutine()
    {
        yield return new WaitForSeconds(3f);
        player.Health = player.MaxHealth;
        UIController.Instance.UpdateHealthBar(player.MaxHealth);
        player.isDead = false;
        player.spriteRenderer.enabled = true;
    }
}
