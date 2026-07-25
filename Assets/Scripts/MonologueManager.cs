using System.Collections;
using TMPro;
using UnityEngine;

public class MonologueManager : MonoBehaviour
{
    public static MonologueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialogueBox; // parent panel, enabled/disabled as needed
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text bodyText;

    private Coroutine activeRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    /// <summary>
    /// Starts playing the given monologue. If one is already playing, it's
    /// stopped and replaced (so triggers can't stack on top of each other).
    /// </summary>
    public void PlayMonologue(Monologue monologue)
    {
        if (monologue == null)
        {
            Debug.LogWarning("MonologueManager: tried to play a null Monologue.");
            return;
        }

        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
        }

        activeRoutine = StartCoroutine(PlayRoutine(monologue));
    }

    /// <summary>Immediately stops whatever monologue is currently playing.</summary>
    public void StopMonologue()
    {
        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
            activeRoutine = null;
        }

        dialogueBox.SetActive(false);
    }

    private IEnumerator PlayRoutine(Monologue monologue)
    {
        yield return new WaitForSeconds(monologue.delayBeforeStart);

        dialogueBox.SetActive(true);
        speakerNameText.text = monologue.speakerName;

        foreach (var line in monologue.lines)
        {
            bodyText.text = line.text;
            yield return new WaitForSeconds(line.displayDuration);
        }

        dialogueBox.SetActive(false);
        activeRoutine = null;
    }
}


