using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public static GoalManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void PlayConfetti(Transform goal)
    {
        if (goal == null) return;

        ParticleSystem confettiFX = goal.GetComponentInChildren<ParticleSystem>();

        if (confettiFX == null) return;

        confettiFX.Play();
    }
}