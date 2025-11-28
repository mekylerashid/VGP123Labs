using UnityEngine;

public class SimplePickUps : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        Powerup
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PickupType pickupType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();

            switch (pickupType)
            {
                case PickupType.Life:
                    //Increase player's life count
                   GameManager.Instance.lives++;
                    break;

                case PickupType.Powerup:
                    //Grant player a power-up
                    pc.ApplyJumpForcePowerup();
                    break;
            }
            //Destroy the pickup after collection
            Destroy(gameObject);
        }
    }
}