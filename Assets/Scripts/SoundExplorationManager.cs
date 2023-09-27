using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundExplorationManager : MonoBehaviour
{
    public static SoundExplorationManager soundExplorationManager;

    [SerializeField]
    AudioSource explorationMusic;

    void Start()
    {
        explorationMusic.playOnAwake = true;
    }
}
