using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace PaintAstic.Scene.MainMenu
{
    public class Setting : MonoBehaviour
    {
        [SerializeField] private Button _sfxButton;
        [SerializeField] private Button _bgmButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TextMeshProUGUI _textSfx;
        [SerializeField] private TextMeshProUGUI _textBgm;


        private void BackButtonListener(Button button, UnityAction listener)
        {

        }
    }

}
