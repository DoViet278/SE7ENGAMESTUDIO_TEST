using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Buttons")]
    public GameObject kickButton;

    [Header("Player")]
    public Transform player;

    [Header("Goals")]
    public Transform goalLeft;
    public Transform goalRight;

    [Header("Balls")]
    public SoccerBall[] balls;

    private SoccerBall nearestBall;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CheckNearestBall();
    }

    void CheckNearestBall()
    {
        nearestBall = null;

        float minDist = 1f;

        foreach (SoccerBall ball in balls)
        {
            if (ball == null || ball.isScored) continue;

            float dist = Vector3.Distance(
                player.position,
                ball.transform.position
            );

            if (dist < minDist)
            {
                minDist = dist;
                nearestBall = ball;
            }
        }

        kickButton.SetActive(nearestBall != null);
    }

    public void KickNearestBall()
    {
        if (nearestBall == null) return;

        Transform goal = GetNearestGoal(nearestBall.transform);

        nearestBall.KickToGoal(goal, 15f);
    }

    public void AutoKick()
    {
        SoccerBall farthestBall = null;

        float maxDist = 0;

        foreach (SoccerBall ball in balls)
        {
            if (ball == null || ball.isScored) continue;

            float dist = Vector3.Distance(
                player.position,
                ball.transform.position
            );

            if (dist > maxDist)
            {
                maxDist = dist;
                farthestBall = ball;
            }
        }

        if (farthestBall == null) return;

        Transform goal = GetNearestGoal(farthestBall.transform);

        farthestBall.KickToGoal(goal, 20f);
    }

    Transform GetNearestGoal(Transform ball)
    {
        float leftDist =
            Vector3.Distance(ball.position, goalLeft.position);

        float rightDist =
            Vector3.Distance(ball.position, goalRight.position);

        return leftDist < rightDist
            ? goalLeft
            : goalRight;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}