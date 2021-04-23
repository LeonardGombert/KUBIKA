using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LaragonBackend : MonoBehaviour
{
    [SerializeField] string url;

    void Start() => StartCoroutine(GetInfo());

    IEnumerator GetInfo()
    {
        WWWForm form = new WWWForm();

        form.AddField("level_name", "Tables");
        
        WWW w = new WWW(url, form);

        yield return w;
        
        if (w.error != null) Debug.Log(w.error.ToString());
        
        else if (w.isDone)
        {
            Debug.Log(w.text.ToString());
            Debug.Log(Application.persistentDataPath);
            File.WriteAllText (Application.persistentDataPath + "/saveload.json", w.text.ToString());
        }


        w.Dispose();
    }
}