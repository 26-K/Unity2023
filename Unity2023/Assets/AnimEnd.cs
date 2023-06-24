using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndAnim()
    {
        this.gameObject.SetActive(false);
    }

}
