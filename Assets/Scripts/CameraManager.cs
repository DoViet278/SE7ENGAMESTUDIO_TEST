using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public CameraFollow cam;

    public Transform player;

    void Awake()
    {
        Instance = this;
    }

    public void FollowBall(Transform ball)
    {
        cam.SetTarget(ball);
    }

    public void FollowPlayer()
    {
        cam.SetTarget(player);
    }
}