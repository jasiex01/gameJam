using DG.Tweening;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new DOTweenAnimationTemplate", menuName = "DOTween Animation Template", order = 0)]
    public class DOTweenAnimationTemplate : ScriptableObject
    {
        [Min(0)]
        public float duration;
        public Ease ease;
    }
}