using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float yOffset = -3;
    public bool follow = false;

    private void Start()
    {
        //yOffset = -3;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (follow)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.35f);
            //transform.position = targetPosition;
        }
    }
}
