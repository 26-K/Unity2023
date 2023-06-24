using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SEDatas seDatas;

    public List<AudioClip> playWaitList = new List<AudioClip>();
    protected override void UnityAwake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (playWaitList.Count >= 1) //一度に同時になる可能性のある効果音はタイミングをずらして鳴らす
        {
            audioSource.PlayOneShot(playWaitList[0]);
            playWaitList.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCardHoverSound()
    {
        audioSource.PlayOneShot(seDatas.cardHover);
    }

    public void PlayCardDrawSound()
    {
        Debug.LogError("まだ効果音をつけていない:{PlayCardDrawSound()}");
        AddPlayWaitSoundEffect(seDatas.drawCard);
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
