using UnityEngine;

public class SwitchCamMode : MonoBehaviour
{
    public FollowPlayer cam;
    public GameObject hud;
    public bool result = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cam.follow = result;
            hud.SetActive(result);
            GameController.startTimer = true;
            Destroy(gameObject);
        }
    }
}
