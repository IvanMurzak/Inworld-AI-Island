using TMPro;
using UnityEngine;
namespace Inworld.Runtime
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField] GameObject m_MessageBox;
        [SerializeField] GameObject m_LoadingPanel;
        [SerializeField] TMP_Text m_LoadingProgress;
        [SerializeField] TMP_Text m_Title;
        [SerializeField] TMP_Text m_Content;
        // Start is called before the first frame update

        public void ShowError(string strTitle, string strContent)
        {
            m_LoadingPanel.SetActive(false);
            m_MessageBox.SetActive(true);
            m_Title.text = strTitle;
            m_Content.text = strContent;
        }
        public void ShowWait(string strProgress)
        {
            m_LoadingPanel.SetActive(true);
            m_MessageBox.SetActive(false);
            m_LoadingProgress.text = $"Loading...{strProgress}%";
        }
    }
}
