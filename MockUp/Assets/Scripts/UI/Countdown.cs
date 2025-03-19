using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening; 

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private readonly string[] countdownNumbers = { "3", "2", "1", "START!" };

    private void Start()
    {
        GameManager.Instance.SetCastorMovement(false);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        for (int i = 0; i < countdownNumbers.Length; i++)
        {
            countdownText.text = countdownNumbers[i];
            AnimateText();

            PlayCountdownSound(i);

            yield return new WaitForSeconds(1f); 
        }

        GameManager.Instance.SetCastorMovement(true);
        Destroy(gameObject); 
    }

    private void AnimateText()
    {
        countdownText.transform.localScale = Vector3.zero; 
        countdownText.DOFade(1, 0.2f); 
        countdownText.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack) 
            .OnComplete(() => countdownText.DOFade(0, 0.5f)); 
    }

    private void PlayCountdownSound(int index)
    {
        switch (index)
        {
            case 0:
                ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Start, volume: 1.0f);
                break;
            case 1:
                ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Start2, volume: 1.0f);
                break;
            case 2:
                ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Start3, volume: 1.0f);
                break;
            case 3:
                ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Start4, volume: 1.0f);
                break;
        }
    }
}
