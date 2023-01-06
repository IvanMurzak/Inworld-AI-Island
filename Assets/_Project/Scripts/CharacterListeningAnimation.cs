using UnityEngine;
using Inworld.Audio;
using Inworld.Packets;

namespace Inworld.Model
{
    public class CharacterListeningAnimation : MonoBehaviour
    {
        #region Inspector Variables
        [SerializeField] protected Animator m_Animator;
        [SerializeField] protected InworldCharacter m_InworldCharacter;
        [SerializeField] protected AudioInteraction m_AudioInteraction;
        #endregion

        void OnEnable()
        {
            if (m_InworldCharacter)
            {
                m_InworldCharacter.onStartListening += OnStartListening;
                m_InworldCharacter.onEndListening += OnEndListening;
            }
            if (m_AudioInteraction)
            {
                m_AudioInteraction.OnAudioStarted += OnAudioPlayingStart;
                m_AudioInteraction.OnAudioFinished += OnAudioFinished;
            }
        }
        private void OnDisable()
        {
            if (m_InworldCharacter)
            {
                m_InworldCharacter.onStartListening -= OnStartListening;
                m_InworldCharacter.onEndListening -= OnEndListening;
            }
            if (m_AudioInteraction)
            {
                m_AudioInteraction.OnAudioStarted -= OnAudioPlayingStart;
                m_AudioInteraction.OnAudioFinished -= OnAudioFinished;
            }
        }

        void OnStartListening(string ID) => m_Animator.SetFloat("Listening", 1);
        void OnEndListening(string ID) => m_Animator.SetFloat("Listening", 0);
        void OnAudioPlayingStart(PacketId packetId)
        {
            m_Animator.SetFloat("Listening", 1);
            m_Animator.SetFloat("Talking", 1);
        }
        void OnAudioFinished() => m_Animator.SetFloat("Talking", 0);
    }
}