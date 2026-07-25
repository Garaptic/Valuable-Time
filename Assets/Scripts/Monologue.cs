using UnityEngine;

[CreateAssetMenu(fileName = "New Monologue", menuName = "Dialogue/Monologue")]
public class Monologue : ScriptableObject
{
    [System.Serializable]
    public class Line
    {
        [TextArea(2, 4)]
        public string text;
        public float displayDuration = 3f; // how long this line stays on screen, in seconds
    }

    public string speakerName;

    [Tooltip("Seconds to wait after PlayMonologue() is called before the first line appears.")]
    public float delayBeforeStart = 2f;

    public Line[] lines;
}