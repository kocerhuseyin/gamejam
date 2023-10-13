using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Animator playerAnimator;
    public float restartDelay = 2.0f; // Ölüm durumunda yeniden baþlatma gecikmesi.

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision();
        }
    }

    // Oyuncu engelle karþýlaþtýðýnda çaðrýlacak ortak iþlev.
    protected virtual void HandlePlayerCollision()
    {
        StartCoroutine(RestartLevelAfterDelay(restartDelay));
    }

    protected IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.RestartLevel();
    }
}
