using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos = -0.09f;
    [SerializeField] private float maxXPos = 237.77f;

    [SerializeField] private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManager.Instance.OnPlayerSpawned += UpdatePlayerRef;
        //MAKE YOUR CODE DEFENSIVE AGAINST BAD INPUT!!
        //if (!target)
        //{
        //    GameObject player = GameObject.FindGameObjectWithTag("Player");
        //    if (!player)
        //    {
        //        Debug.LogError("CameraFollow: No GameObject with tag 'Player' found in the scene.");
        //        return;
        //    }
        //    target = player.transform;
        //}
    }

    private void UpdatePlayerRef(PlayerController playerInstance)
    {
        target = playerInstance.transform;
    }
    // Update is called once per frame
    void Update()
    {
        //exit parameters that will stop the function from continuing if these things happpen
        if(!target) return;

        //store our current position
        Vector3 pos = transform.position;
        //uodate the x position to match the target's x position
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        //apply the updated position back to the transform
        transform.position = pos;
    }
}
