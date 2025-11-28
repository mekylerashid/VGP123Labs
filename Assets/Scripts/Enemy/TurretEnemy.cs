using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2.0f;
    private float timeSinceLastFire = 0;
    [SerializeField] protected Collider2D rangeLeft;
    [SerializeField] protected Collider2D rangeRight;
    protected bool inRange = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        if (fireRate<=0)
        {
            Debug.LogError("Fire rate must be greater than zero, set to default value of 2");
            fireRate = 2.0f;
        }
    }

    // Update is called once per frame
    public void OnLeftRangeEnter()
    {
        sr.flipX = true;
        inRange = true;
    }

    public void OnRightRangeEnter()
    {
        sr.flipX = false;
        inRange = true;
    }

    public void OnPlayerExit()
    {
        inRange = false;
    }
    void Update()
    {
       
        if (inRange == true)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("TurretIdle"))
            {
                //trigger fire animation logic
                if (Time.time >= timeSinceLastFire + fireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                }
            }
        }
    }
}
