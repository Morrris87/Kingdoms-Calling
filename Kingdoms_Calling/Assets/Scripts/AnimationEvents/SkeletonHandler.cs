using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHandler : MonoBehaviour
{

    public AudioSource skeletonAudioSource;
    public AudioClip basicAttackClip;
    public AudioClip walkingClip;
    private float startTime;
    private float startClipTime;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAudioSource = GetComponentInParent<AudioSource>();
        startTime = 0;
        startClipTime = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
    }

    public void SkeletonWalkClipEvent()
    {
        if (startTime >= startClipTime)
        {
            if (basicAttackClip != null)
            {
                skeletonAudioSource.clip = walkingClip;
                skeletonAudioSource.Play();
            }
        }
    }

    public void BasicAttackClipEvent()
    {
        if (basicAttackClip != null)
        {
            skeletonAudioSource.clip = basicAttackClip;
            skeletonAudioSource.Play();
        }
    }


}
