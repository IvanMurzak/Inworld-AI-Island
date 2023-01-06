using UnityEngine;
using UniRx;

namespace Inworld.Model
{
    public class CharacterListeningAnimation : MonoBehaviour
    {
        #region Inspector Variables
        [SerializeField] protected Animator m_Animator;
        [SerializeField] protected InworldCharacter m_InworldCharacter;
        #endregion

        void Start()
        {
            if (m_InworldCharacter)
            {
                m_InworldCharacter.OnStartListening
                    .Subscribe(id => m_Animator.SetFloat("Listening", 1f))
                    .AddTo(this);

                m_InworldCharacter.OnEndListening
                    .Subscribe(id => m_Animator.SetFloat("Listening", 0f))
                    .AddTo(this);
            }
        }
    }
}