using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropForTomatoes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (!collision.gameObject.CompareTag("Bullet")) return;

        AudienceTomatoes.Instance.ThrowTomatoesToPlayerPosition();

        Destroy(gameObject);
    }
}
