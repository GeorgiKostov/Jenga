using System;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class EventBroadcaster:MonoBehaviour
    {
        public static Action OnTestMyStack;

        public void OnClick()
        {
            OnTestMyStack?.Invoke();
        }
    }
}