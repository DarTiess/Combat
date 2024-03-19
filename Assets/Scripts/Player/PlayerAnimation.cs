using TMPro;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerAnimation : IMoveAnimation
    {
        private readonly Animator _animator;
        private static readonly int IS_MOVE = Animator.StringToHash("IsMove");

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void MoveAnimation(float speed)
        {
            _animator.SetFloat(IS_MOVE, speed);
        }
    }
}