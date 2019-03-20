using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PhaseManager : MonoBehaviour {
    [Serializable]
    public class Phase {
        public Color backgroundColor;
        public float phaseDuration;
    }

    [SerializeField]
    private Sprite background;

    [SerializeField]
    private Sprite backgroundTransition;

    [SerializeField]
    private float scrollSpeed;

    [SerializeField]
    private List<Phase> phases = new List<Phase>();
    private int currentPhaseIndex = 0;

    List<GameObject> bgInstances = new List<GameObject>();

    public int GetCurrentPhaseIndex() {
        return currentPhaseIndex;
    }

    public Phase GetCurrentPhase() {
        return phases[currentPhaseIndex];
    }

    private void AddBgInstance(Sprite sprite) {
        // Set all the properties
        GameObject obj = new GameObject();
        obj.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.color = GetCurrentPhase().backgroundColor;

        // Set it to around the screen size
        double width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
        double height = Camera.main.orthographicSize * 2.0;

        obj.transform.localScale = new Vector3((float)width, (float)height) / 5.0f;

        // If there already is an instance, place it above it
        if (bgInstances.Count > 0) {
            float newY = bgInstances[bgInstances.Count - 1].transform.position.y;
            newY += (obj.GetComponent<SpriteRenderer>().bounds.extents.y * 2.0f);
            obj.transform.position = new Vector3(0.0f, newY);
        }

        bgInstances.Add(obj);
    }

    IEnumerator PhaseRoutine() {
        while (currentPhaseIndex < phases.Count) {
            yield return new WaitForSeconds(GetCurrentPhase().phaseDuration);
            AddBgInstance(backgroundTransition);
            ++currentPhaseIndex;
        }
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(PhaseRoutine());

        // Add initial backgrounds
        AddBgInstance(background);
        AddBgInstance(background);
    }
    
    // Update is called once per frame
    void FixedUpdate() {
        // Check if out-of-bounds
        for (int i = 0; i < bgInstances.Count; ++i) {
            if (bgInstances[i].transform.position.y < -10.0f) {
                Destroy(bgInstances[i]);
                bgInstances.RemoveAt(i);

                // Add next bg
                AddBgInstance(background);
            }
        }

        // Move down
        foreach (GameObject obj in bgInstances) {
            obj.transform.position -= new Vector3(0.0f, scrollSpeed * Time.deltaTime);
        }
    }
}
