using TMPro;
using UnityEngine;

namespace Sumo.UI
{
    public class UIUserItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Initialize(string userName, int score)
        {
            nameText.text = userName;
            scoreText.text = $"{score}";
        }
    }
}
