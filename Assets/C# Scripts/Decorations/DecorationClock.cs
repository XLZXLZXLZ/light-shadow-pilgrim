using UnityEngine;
using DG.Tweening;

public class DecorationClock : MonoBehaviour
{
    public Transform secondHand;
    public Transform minuteHand;

    void Start()
    {
        InvokeRepeating("RotateHands", 0f,1f);
    }

    void RotateHands()
    {
        RotateHand(secondHand, 30f);
        RotateHand(minuteHand, 2.5f);
    }

    void RotateHand(Transform hand, float angle)
    {
        hand.DOBlendableLocalRotateBy( new Vector3(0, angle, 0), 0.1f)
            .SetEase(Ease.OutBack);
    }
}