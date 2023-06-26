using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : SingletonMonoBehaviour<NextScene>
{
    float time = 2.5f;
    bool isEnd = false;


    protected override void UnityAwake()
    {
    }

    public void Update()
    {
        time -= Time.deltaTime;
        if (isEnd)
        {
            return;
        }
        if (time <= 0.0f)
        {
            isEnd = true;
            SceneManager.LoadScene("TitleScene");
        }
    }


}
