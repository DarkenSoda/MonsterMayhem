using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinUIText : MonoBehaviour
{
    [SerializeField] private YouWinUI youWinUI;

    private void Start() {
        Hide();
    }


    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void WinTimer() {
        StartCoroutine(OnWinMenuUI());
    }

    private IEnumerator OnWinMenuUI() {
        yield return new WaitForSeconds(2f);

        youWinUI.Show();
        Time.timeScale = 0f;
    }
}
