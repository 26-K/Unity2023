using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseSO : ScriptableObject
{
    public List<Image> rarityImages;
    public Image GetCardBackImage(int rarity)
    {
        if (rarity > rarityImages.Count)
        {
            rarity = rarityImages.Count;
        }
        return rarityImages[rarity - 1];
    }
}
