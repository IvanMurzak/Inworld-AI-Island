/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using Inworld.Util;
using System.Collections.Generic;
using UnityEngine;
namespace Inworld.Sample.UI
{
    /// <summary>
    ///     This class is used to show/hide the Inworld Characters' floating text bubble.
    /// </summary>
    public class ChatPanel3D : MonoBehaviour
    {
        //TODO(Yan): Use ObjPool to replace instantiations.
        readonly Dictionary<string, ChatBubble> m_Bubbles = new Dictionary<string, ChatBubble>();

        void OnEnable()
        {
            _ClearHistoryLog();
            m_Owner.Event.AddListener(OnInteractionStatus);
        }
        void OnInteractionStatus(InteractionStatus status, List<HistoryItem> historyItems)
        {
            if (status != InteractionStatus.HistoryChanged)
                return;
            _RefreshBubbles(historyItems);
        }
        void _RefreshBubbles(List<HistoryItem> historyItems)
        {
            foreach (HistoryItem item in historyItems)
            {
                if (!m_Bubbles.ContainsKey(item.UtteranceId))
                {
                    if (item.Event.Routing.Source.IsPlayer() && item.Event.Routing.Target.Id == m_Owner.ID)
                    {
                        m_Bubbles[item.UtteranceId] = Instantiate(m_LeftBubble, m_PanelAnchor);
                        m_Bubbles[item.UtteranceId].CharacterName = InworldAI.User.Name;

                    }
                    else if (item.Event.Routing.Source.IsAgent() && item.Event.Routing.Source.Id == m_Owner.ID)
                    {
                        m_Bubbles[item.UtteranceId] = Instantiate(m_RightBubble, m_PanelAnchor);
                        m_Bubbles[item.UtteranceId].CharacterName = m_Owner.CharacterName;
                    }
                }
                m_Bubbles[item.UtteranceId].Text = item.Event.Text;
            }
        }
        void _ClearHistoryLog()
        {
            foreach (KeyValuePair<string, ChatBubble> kvp in m_Bubbles)
            {
                Destroy(kvp.Value.gameObject, 0.25f);
            }
            m_Bubbles.Clear();
        }

        #region Inspector Variables
        [SerializeField] ChatBubble m_LeftBubble;
        [SerializeField] ChatBubble m_RightBubble;
        [SerializeField] RectTransform m_PanelAnchor;
        [SerializeField] InworldCharacter m_Owner;
        #endregion
    }
}
