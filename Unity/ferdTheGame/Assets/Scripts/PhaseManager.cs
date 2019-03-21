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
    bool prevWasShift = false;

    List<GameObject> bgInstances = new List<GameObject>();

    public int GetCurrentPhaseIndex() {
        return currentPhaseIndex;
    }

    public Phase GetCurrentPhase() {
        return phases[currentPhaseIndex];
    }

    private void AddBgInstance(bool phaseShift = false) {
        // Set all the properties
        GameObject obj = new GameObject();
        obj.transform.parent = transform;

        string partName = (phaseShift) ? "Fade" : "Solid";
        obj.name = $"BG Phase {currentPhaseIndex}, {partName}";
        obj.AddComponent<SpriteRenderer>();

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sprite = (phaseShift) ? backgroundTransition : background;
        sr.color = GetCurrentPhase().backgroundColor;

        // Set it to around the screen size
        double width = Camera.main.orthographicSize * Screen.width / Screen.height;
        obj.transform.localScale = new Vector2((float)width, 1.0f);
        
        // If there already is an instance, place it above it
        if (bgInstances.Count > 0) {
            float newY = 0.0f;

            if (!prevWasShift) {
                newY = bgInstances[bgInstances.Count - 1].transform.position.y;
            } else {
                // Don't use the phase shift Y because it's larger than the solid bg
                newY = bgInstances[bgInstances.Count - 2].transform.position.y;
                newY += background.bounds.extents.y * 2.0f;
            }
            
            if (phaseShift) {
                newY += backgroundTransition.bounds.extents.y + background.bounds.extents.y;
            } else {
                newY += background.bounds.extents.y * 2.0f;
            }

            float newZ = 0.0f;
            if (prevWasShift) {
                newZ = bgInstances[bgInstances.Count - 1].transform.position.z + 1.0f;
            } else if (phaseShift) {
                newZ = -1.0f;
            }

            obj.transform.position = new Vector3(0.0f, newY, newZ);
        }

        prevWasShift = phaseShift;
        bgInstances.Add(obj);
    }

    IEnumerator PhaseRoutine() {
        while (currentPhaseIndex < phases.Count) {
            yield return new WaitForSeconds(GetCurrentPhase().phaseDuration);
            AddBgInstance(true);
            ++currentPhaseIndex;
        }

        // Make it so the index is at the last phase
        --currentPhaseIndex;
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(PhaseRoutine());

        // Add initial backgrounds
        // @HARDCODED: 4 backgrounds seem to fill the screen
        AddBgInstance();
        bgInstances[0].transform.position -= new Vector3(0.0f, 5.0f);

        AddBgInstance();
        AddBgInstance();
        AddBgInstance();
    }
    
    // Update is called once per frame
    void FixedUpdate() {
        // Check if out-of-bounds
        for (int i = 0; i < bgInstances.Count; ++i) {
            if (bgInstances[i].transform.position.y < -10.0f) {
                Destroy(bgInstances[i]);
                bgInstances.RemoveAt(i);

                // Add next bg, dirty check name if solid
                if (bgInstances[i].name.Contains("Solid")) {
                    AddBgInstance();
                }
            }
        }

        // Move down
        foreach (GameObject obj in bgInstances) {
            obj.transform.position -= new Vector3(0.0f, scrollSpeed * Time.deltaTime);
        }
    }
}
