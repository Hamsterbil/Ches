using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public List<AudioClip> audioClips;
    [SerializeField] private AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(string type)
    {
        switch(type)
        {
            case "win":
                audioSource.clip = audioClips[0];
                break;
            case "lose":
                audioSource.clip = audioClips[1];
                break;
            case "pieceClick":
                audioSource.clip = audioClips[2];
                break;
            case "invalidMove":
                audioSource.clip = audioClips[3];
                break;
            case "hover":
                audioSource.clip = audioClips[4];
                break;
        }
        audioSource.Play();
    }

}
