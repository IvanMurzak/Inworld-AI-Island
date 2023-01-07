using UnityEngine;
using Inworld.Audio;
using Inworld.Packets;
using Inworld;

namespace _Project.Character
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
                m_InworldCharacter.onStartPlayerTalking += OnStartListening;
                m_InworldCharacter.onEndPlayerTalking += OnEndListening;

                //m_InworldCharacter.onStartListening += OnStartListening;
                //m_InworldCharacter.onEndListening += OnEndListening;
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
                m_InworldCharacter.onStartPlayerTalking -= OnStartListening;
                m_InworldCharacter.onEndPlayerTalking -= OnEndListening;

                //m_InworldCharacter.onStartListening -= OnStartListening;
                //m_InworldCharacter.onEndListening -= OnEndListening;
            }
            if (m_AudioInteraction)
            {
                m_AudioInteraction.OnAudioStarted -= OnAudioPlayingStart;
                m_AudioInteraction.OnAudioFinished -= OnAudioFinished;
            }
        }

        void OnStartListening()
        {
            Debug.Log("<b><color=green>CharacterListeningAnimation.OnStartListening</color></b>");
            m_Animator.SetFloat("Listening", 1);
        }
        void OnEndListening(string text)
        {
            Debug.Log("<b><color=red>CharacterListeningAnimation.OnEndListening</color></b>");
            m_Animator.SetFloat("Listening", 0);
        }
        void OnAudioPlayingStart(PacketId packetId)
        {
            m_Animator.SetFloat("Listening", 0);
            m_Animator.SetFloat("Talking", 1);
        }
        void OnAudioFinished() => m_Animator.SetFloat("Talking", 0);
    }
}