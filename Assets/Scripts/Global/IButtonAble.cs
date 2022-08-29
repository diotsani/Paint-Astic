using UnityEngine.Events;
using UnityEngine.UI;

namespace PaintAstic.Global
{
    public interface IButtonAble
    {
        void SetButtonListener(Button button, UnityAction listener);
        void SetAllButtonListener();
    }
}
