using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    [LabelText("ID")] public int id;
    [LabelText("ボーナス名")] public string displayName;
    [LabelText("レアリティ")] public int rarity;
    [LabelText("画像")] [PreviewField(150, ObjectFieldAlignment.Center)] public Sprite sprite;
    [LabelText("ルーレットから出てこない内部的な扱いのアビリティにするフラグ")] public bool isHideScreen;
    public abstract void Effect(CharacterBase character = null); //アビリティの効果、上書きして使う
    public abstract void AcquisitionEffect(CharacterBase character = null); //アビリティを習得した瞬間にのみ発動する効果、ヒール等に使う。
    public virtual int GetEffectValue() { return 0; } //アビリティの効果の強さ、一部アビリティにしか使われない
    public virtual float GetEffectValueFloat() { return 0.0f; } //アビリティの効果の強さ、一部アビリティにしか使われない
}
