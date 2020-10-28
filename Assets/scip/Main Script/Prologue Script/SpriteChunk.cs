using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Animation
{
    [Serializable]
    public class SpriteChunk
    {
        /// <summary>
        /// 이 애니메이션의 이름
        /// </summary>
        [SerializeField]
        private string animationName;

        /// <summary>
        /// 현재 이 애니메이션이 돌아가는가?
        /// </summary>
        [SerializeField]
        private bool isPlaying;

        /// <summary>
        /// 프레임마다 텀 시간
        /// </summary>
        [SerializeField]
        private float delayTime;

        /// <summary>
        /// 반복할 것인가?
        /// </summary>
        [SerializeField]
        private bool loop;

        /// <summary>
        /// 애니메이션 리스트
        /// </summary>
        [SerializeField]
        private List<Sprite> animationList;

        public bool Loop { get { return loop; } }
        public float DelayTime { get { return delayTime; } }
        public bool IsPlaying { get { return isPlaying; } set { isPlaying = value; } }
        public string AnimationName { get { return animationName; } }
        public Sprite GetAnimationFrame(int frame)
        {
            if (frame >= animationList.Count)
                return null;

            return animationList[frame];
        }
    }
}