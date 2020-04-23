using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.startOver();
        }
    }

    private void startOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
