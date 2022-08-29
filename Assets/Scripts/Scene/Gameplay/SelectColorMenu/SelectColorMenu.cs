using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Colors
{
    public class SelectColorMenu : MonoBehaviour
    {
        [SerializeField] private List<Color> listColors;

        public List<Color> ListColors => listColors;

        private void Reset()
        {
            listColors = new List<Color>();
            listColors.Add(Color.red);
            listColors.Add(Color.cyan);
            listColors.Add(Color.blue);
            listColors.Add(Color.yellow);
        }
    }
}