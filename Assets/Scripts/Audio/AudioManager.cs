using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private FMOD.Studio.EventInstance thrustInstance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Audio Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string audioLibrarySourceSound, Vector3 position)
    {
        RuntimeManager.PlayOneShot(audioLibrarySourceSound, position);
    }

    private void Update()
    {

    }


}
