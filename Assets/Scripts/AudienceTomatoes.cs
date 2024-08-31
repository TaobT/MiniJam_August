using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script manages the spawning of tomatoes from the audience
public class AudienceTomatoes : MonoBehaviour
{
    public static AudienceTomatoes Instance;
    [Header("Prefabs")]
    [SerializeField] private GameObject tomatoProjectilePrefab;

    [Header("Configuration")]
    // shootingYPoint indicates from where the tomatoes will be thrown (instantiated)
    [SerializeField] private Transform shootingYPoint;
    // shootingXRange indicates the range across the x-axis where the tomatoes will be thrown
    [SerializeField] private float shootingXRange;
    
    [SerializeField] private int minPickableTomatoesThrow;
    [SerializeField] private int maxPickableTomatoesThrow;

    [SerializeField] private int minSplattedTomatoesThrow; // Splatted tomatoes just for visuals ;)
    [SerializeField] private int maxSplattedTomatoesThrow;

    [SerializeField] private float tomatoLandingRadio; // The radius where the tomatoes will land around the player

    [Header("References")]
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        Instance = this;
    }

    public void ThrowTomatoesToPlayerPosition()
    {
        int pickableTomatoesToThrow = Random.Range(minPickableTomatoesThrow, maxPickableTomatoesThrow);

        for (int i = 0; i < pickableTomatoesToThrow; i++)
        {
            Vector2 tomatoPosition = new Vector2(Random.Range(shootingYPoint.position.x - shootingXRange, shootingYPoint.position.x + shootingXRange), shootingYPoint.position.y);
            Vector2 tomatoLandingPosition = new Vector2(playerTransform.position.x + Random.Range(-tomatoLandingRadio, tomatoLandingRadio), playerTransform.position.y + Random.Range(-tomatoLandingRadio, tomatoLandingRadio));

            GameObject tomato = Instantiate(tomatoProjectilePrefab, tomatoPosition, Quaternion.identity);
            tomato.GetComponent<TomatoProjectile>().SetLandingPosition(tomatoLandingPosition, false);
        }

        int splattedTomatoesToThrow = Random.Range(minSplattedTomatoesThrow, maxSplattedTomatoesThrow);

        for (int i = 0; i < splattedTomatoesToThrow; i++)
        {
            Vector2 tomatoPosition = new Vector2(Random.Range(shootingYPoint.position.x - shootingXRange, shootingYPoint.position.x + shootingXRange), shootingYPoint.position.y);
            Vector2 tomatoLandingPosition = new Vector2(playerTransform.position.x + Random.Range(-tomatoLandingRadio, tomatoLandingRadio), playerTransform.position.y + Random.Range(-tomatoLandingRadio, tomatoLandingRadio));
            GameObject tomato = Instantiate(tomatoProjectilePrefab, tomatoPosition, Quaternion.identity);
            tomato.GetComponent<TomatoProjectile>().SetLandingPosition(tomatoLandingPosition, true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, tomatoLandingRadio);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(shootingYPoint.position.x - shootingXRange, shootingYPoint.position.y), new Vector2(shootingYPoint.position.x + shootingXRange, shootingYPoint.position.y));
    }
}
