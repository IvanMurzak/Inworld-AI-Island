using Inworld.Util;
using UnityEngine;


namespace Inworld.Model.Sample
{
    public class OvrLipAnimations : MonoBehaviour, ILipAnimations
    {
        [SerializeField] OVRLipSyncContext m_Context;
        
        [Tooltip("For SkinMeshRender generated via GLTFUtility, Viseme order is correct.")]
        [SerializeField] int m_VisemeIndex = 57; 
        [SerializeField] int m_VisemeLength = 15;
        [SerializeField] AudioSource m_Audio;
        [SerializeField] SkinnedMeshRenderer m_Skin;
        [Range(1, 100)] [SerializeField] int m_SmoothCount = 70;
        [SerializeField] bool m_IsDefaultSkin = true;
        public void ConfigureModel(GameObject model)
        {
            if (!m_Skin)
            {
                m_Skin = model.GetComponentInChildren<SkinnedMeshRenderer>();
                m_IsDefaultSkin = false;
                _MappingBlendShape();
            }
            m_Context.audioSource = m_Audio;
            m_Context.enabled = false;
        }
        void Awake()
        {
            if (m_Skin)
                _MappingBlendShape();
        }
        void _MappingBlendShape()
        {
            if (!m_Skin)
                return;
            for (int i = 0; i < m_Skin.sharedMesh.blendShapeCount; i++)
            {
                if (m_Skin.sharedMesh.GetBlendShapeName(i) != "viseme_sil")
                    continue;
                m_VisemeIndex = i;
                Debug.Log($"Find Viseme Index {m_VisemeIndex}");
                break;
            }
        }
        void Update()
        {
            if (!_IsValid)
                return;
            OVRLipSync.Frame frame = m_Context.GetCurrentPhonemeFrame();
            if (frame != null)
            {
                for (int i = 0; i < m_VisemeLength; i++)
                {
                    int skinWeight = m_IsDefaultSkin ? 100 : 1;
                    m_Skin.SetBlendShapeWeight(m_VisemeIndex + i, frame.Visemes[i] * skinWeight);
                }
            }
            // Update smoothing value
            m_Context.Smoothing = m_SmoothCount;
        }
        public void StartLipSync()
        {
            m_Context.enabled = true;
        }
        public void StopLipSync()
        {
            m_Context.enabled = false;
        }
        bool _IsValid => m_Skin && m_Context && m_Audio;
    }
}
