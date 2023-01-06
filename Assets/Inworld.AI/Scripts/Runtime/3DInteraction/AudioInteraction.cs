/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using Inworld.Packets;
using System;
using System.Collections.Concurrent;
using UnityEngine;
namespace Inworld.Audio
{
    /// <summary>
    ///     This component is used to receive/send audio from server.
    /// </summary>
    public class AudioInteraction : MonoBehaviour
    {
        #region Callbacks
        void OnPacketEvents(InworldPacket packet)
        {
            if (packet.Routing.Target.Id != m_Character.ID && packet.Routing.Source.Id != m_Character.ID)
                return;
            if (packet is not AudioChunk audioChunk)
                return;
            m_AudioChunksQueue.Enqueue(audioChunk);
        }
        #endregion

        /// <summary>
        ///     Call this func to clean up cached queue.
        /// </summary>
        public void Clear()
        {
            m_AudioChunksQueue.Clear();
        }

        #region Inspector Variables
        [SerializeField] InworldCharacter m_Character;
        [SerializeField] AudioSource m_PlaybackSource;
        #endregion

        #region Private Properties Variables
        readonly ConcurrentQueue<AudioChunk> m_AudioChunksQueue = new ConcurrentQueue<AudioChunk>();
        const float kFixedUpdatePeriod = 0.1f;
        float m_CurrentFixedUpdateTime;
        public event Action<PacketId> OnAudioStarted;
        public event Action OnAudioFinished;
        bool _IsAudioPlaying => PlaybackSource != null && PlaybackSource.isPlaying;
        float CurrentAudioLength
        {
            get => Character ? Character.CurrentAudioRemainingTime : 0f;
            set
            {
                if (!Character)
                    return;
                Character.CurrentAudioRemainingTime = value;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        ///     Get/Set its attached Inworld Character.
        /// </summary>
        public InworldCharacter Character
        {
            get => m_Character;
            set
            {
                m_Character = value;
                m_Character.Audio = this;
            }
        }
        /// <summary>
        ///     Get/Set the Audio Source for play back.
        /// </summary>
        public AudioSource PlaybackSource
        {
            get => m_PlaybackSource;
            set => m_PlaybackSource = value;
        }
        #endregion

        #region MonoBehavior Functions
        void Start()
        {
            InworldController.Instance.OnPacketReceived += OnPacketEvents;
            m_PlaybackSource.Stop();
        }
        void Update()
        {
            _TimerCountDown();
            _TryGetAudio();
        }
        void OnDisable()
        {
            if (InworldController.Instance)
                InworldController.Instance.OnPacketReceived -= OnPacketEvents;
        }
        #endregion

        #region Private Functions
        void _TimerCountDown()
        {
            if (CurrentAudioLength <= 0)
                return;
            CurrentAudioLength -= Time.deltaTime;
            if (CurrentAudioLength > 0)
                return;
            CurrentAudioLength = 0;
            OnAudioFinished?.Invoke();
        }
        void _TryGetAudio()
        {
            m_CurrentFixedUpdateTime += Time.deltaTime;
            if (m_CurrentFixedUpdateTime <= kFixedUpdatePeriod)
                return;
            m_CurrentFixedUpdateTime = 0f;
            if (_IsAudioPlaying || !m_AudioChunksQueue.TryDequeue(out AudioChunk chunk) || !m_Character.IsAudioChunkAvailable(chunk.PacketId))
                return;
            AudioClip audioClip = WavUtility.ToAudioClip(chunk.Chunk.ToByteArray());
            if (audioClip)
            {
                CurrentAudioLength = audioClip.length;
                PlaybackSource.PlayOneShot(audioClip, 1f);
            }
            OnAudioStarted?.Invoke(chunk.PacketId);
        }
        #endregion
    }
}
