using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SEDatas seDatas;

    public List<AudioClip> playWaitList = new List<AudioClip>();
    int waitFlame = 0;
    protected override void UnityAwake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        waitFlame--;
        if (playWaitList.Count >= 1 && waitFlame <= 0) //一度に同時になる可能性のある効果音はタイミングをずらして鳴らす
        {
            waitFlame = 2;
            audioSource.pitch = Random.Range(0.9f,1.1f);
            audioSource.PlayOneShot(playWaitList[0]);
            playWaitList.RemoveAt(0);
        }
    }

    public void PlayCardHoverSound()
    {
        audioSource.PlayOneShot(seDatas.cardHover);
    }

    public void PlayCardDrawSound()
    {
        AddPlayWaitSoundEffect(seDatas.drawCard);
    }
    public void PlayNailHitSound()
    {
        AddPlayWaitSoundEffect(seDatas.nailHit);
    }
    public void PlayWoodHitSound()
    {
        AddPlayWaitSoundEffect(seDatas.wallHit);
    }

    void AddPlayWaitSoundEffect(AudioClip audioClip)
    {
        int maxWaitSoundEffectCount = 15;
        if (playWaitList.Count >= maxWaitSoundEffectCount) //連続でならす効果音待ちが多すぎる場合鳴らさない
        {
            return;
        }
        playWaitList.Add(audioClip);
    }
}
