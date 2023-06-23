using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SEDatas seDatas;
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
        
    }

    public void PlayCardHoverSound()
    {
        audioSource.PlayOneShot(seDatas.cardHover);
    }
}
