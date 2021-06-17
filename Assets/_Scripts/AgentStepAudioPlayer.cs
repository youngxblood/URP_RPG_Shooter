using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AgentStepAudioPlayer : MonoBehaviour
{
    protected AudioSource audioSource;
    [SerializeField]
    protected float pitchVariance = 0.05f;
    protected float basePitch;

    [SerializeField]
    protected AudioClip stepClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //Sets the basePitch to the clips pitch
        basePitch = audioSource.pitch;
    }

    protected void PlayClipWithVariablePitch(AudioClip clip)
    {
        var randomPitch = UnityEngine.Random.Range(-pitchVariance, pitchVariance);
        audioSource.pitch = basePitch + randomPitch;
        PlayClip(clip);
    }

    protected void PlayClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayStepSound()
    {
        PlayClipWithVariablePitch(stepClip);
    }
}
