using UnityEngine;
using TMPro;

namespace TheAshBot.SceenManagment
{
    public class LoadingSceneManager : MonoBehaviour
    {


        [SerializeField] private TextMeshProUGUI loadingText;


        private int dotCount;
        private float addDotTimer;
        private float addDotTimerMax = 1;


        private void Update()
        {
            addDotTimer += Time.deltaTime;

            if (addDotTimer > addDotTimerMax)
            {
                addDotTimer = 0;

                loadingText.text = "LOADING" + new string('.', dotCount);

                dotCount++;
                if (dotCount > 3)
                {
                    dotCount = 0;
                }
            }

        }


    }
}
