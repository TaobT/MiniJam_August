using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropForTomatoes : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PropSpawner _spawner;

    private void Start()
    {
        _animator.Play("Spawning");
    }

    public void SetSpawner(PropSpawner spawner)
    {
        _spawner = spawner;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (!collision.gameObject.CompareTag("Bullet")) return;

        AudienceTomatoes.Instance.ThrowTomatoesToPlayerPosition();
        _spawner.PropDied();
        Destroy(gameObject);
    }
}
