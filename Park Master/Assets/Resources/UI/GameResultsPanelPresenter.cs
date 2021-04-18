using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameResultsPanelPresenter : UIElementBase
    {
        [SerializeField] private Text ResultText;
        [SerializeField] private Button RestartButton;
        [SerializeField] private GameObject Holder;

        private void Awake()
        {
            RestartButton.OnClickAsObservable().Subscribe(unit => 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // todo move it to something like GameRestarter / LevelManagement 
            }).AddTo(OnDestroyDisposables);
        }

        public void Show(string text)
        {
            ResultText.text = text;
            Holder.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Holder.gameObject.SetActive(false);
        }

    }
}