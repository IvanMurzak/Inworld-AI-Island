using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MorphState
{
    public string morphName;
    public float morphWeight;
}
[Serializable]
public class FacialAnimation
{
    public string emotion;
    public List<MorphState> morphStates;
}
public class FacialAnimationData : ScriptableObject
{
    public List<FacialAnimation> emotions;
}
