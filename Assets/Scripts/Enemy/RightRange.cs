using UnityEngine;

public class RangeRightTrigger : MonoBehaviour
{
    [SerializeField] protected TurretEnemy turret;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            turret.OnRightRangeEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            turret.OnPlayerExit();
    }
}
