using UnityEngine;
using System.Collections;

public class SoccerBall : MonoBehaviour
{
    private Rigidbody rb;

    public bool isFlying = false;
    public bool isScored = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void KickToGoal(Transform goal, float force)
    {
        if (isFlying || isScored) return;

        StartCoroutine(KickRoutine(goal, force));
    }

    IEnumerator KickRoutine(Transform goal, float force)
    {
        isFlying = true;

        rb.velocity= Vector3.zero;

        Vector3 dir = (goal.position - transform.position).normalized;

        dir.y = 0.4f;

        rb.AddForce(dir * force, ForceMode.Impulse);

        CameraManager.Instance.FollowBall(transform);

        yield return new WaitForSeconds(2f);

        CameraManager.Instance.FollowPlayer();

        isFlying = false;

    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            isScored = true;

            GoalManager.Instance.PlayConfetti(other.transform);
        }
    }
}