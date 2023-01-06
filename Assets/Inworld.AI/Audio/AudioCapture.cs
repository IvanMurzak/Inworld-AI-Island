using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Inworld
{
    public class AudioCapture
    {
        /**
         * Used for most voice based audio.
         */
        const int k_AudioRate = 16000;
        // Size of audioclip used to collect information, need to be big enough to keep up with collect. 
        const int k_BufferSeconds = 1;
        const int k_BufferSize = k_BufferSeconds * k_AudioRate;
        const int k_SizeofInt16 = sizeof(short);
        readonly byte[] m_ByteBuffer = new byte[k_BufferSize * 1 * k_SizeofInt16];
        readonly float[] m_FloatBuffer = new float[k_BufferSize * 1];
        readonly AudioClip m_Recording;
        // Last known position in AudioClip buffer.
        int m_Last;
        public AudioCapture()
        {
            m_Recording = Microphone.Start(null, true, k_BufferSeconds, k_AudioRate);
            m_Last = Microphone.GetPosition(null);
        }
        public bool IsEnabled { get; set; }
        public bool IsCapturing { get; set; } = true;
        // Should be called from time to time to collect audio chunks.
        // Returns new audio chunk collected from last time it is called or null if it is impossible.
        public bool Collect(out ByteString chunk)
        {
            if (IsEnabled && IsCapturing)
            {
                int nPosition = Microphone.GetPosition(null);
                if (nPosition < m_Last)
                    nPosition = k_BufferSize;
                if (nPosition > m_Last)
                {
                    int nSize = nPosition - m_Last;
                    if (m_Recording.GetData(m_FloatBuffer, m_Last))
                    {
                        m_Last = nPosition % k_BufferSize;
                        ConvertAudioClipDataToInt16ByteArray(m_FloatBuffer, nSize * m_Recording.channels, m_ByteBuffer);
                        chunk = ByteString.CopyFrom(m_ByteBuffer, 0, nSize * m_Recording.channels * k_SizeofInt16);
                        return true;
                    }
                }
            }
            chunk = null;
            return false;
        }

        static void ConvertAudioClipDataToInt16ByteArray(IReadOnlyList<float> input, int size, byte[] output)
        {
            MemoryStream dataStream = new MemoryStream(output);

            int i = 0;
            while (i < size)
            {
                dataStream.Write(BitConverter.GetBytes(Convert.ToInt16(input[i] * short.MaxValue)), 0, k_SizeofInt16);
                ++i;
            }

            dataStream.Dispose();
        }

        public void Destroy()
        {
            Microphone.End(null);
            IsEnabled = false;
        }
    }
}
