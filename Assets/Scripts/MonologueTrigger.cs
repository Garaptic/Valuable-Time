using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    [SerializeField] private Monologue monologueToPlay;
    [SerializeField] private bool playOnStart = true;

    private void Start()
    {
        // Start() runs once, right after this scene has finished loading —
        // so this is naturally "after the scene switches to this one."
        if (playOnStart)
        {
            Play();
        }
    }

    /// <summary>
    /// Call this manually instead of using playOnStart, e.g. from a
    /// trigger collider (OnTriggerEnter2D) or a cutscene event.
    /// </summary>
    public void Play()
    {
        if (monologueToPlay == null)
        {
            Debug.LogWarning($"MonologueTrigger on {name} has no Monologue assigned.");
            return;
        }

        MonologueManager.Instance.PlayMonologue(monologueToPlay);
    }
}