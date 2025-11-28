using UnityEngine;

public class RangeLeftTrigger : MonoBehaviour
{
    [SerializeField] protected TurretEnemy turret;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            turret.OnLeftRangeEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            turret.OnPlayerExit();
    }
}

