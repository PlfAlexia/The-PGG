using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleFileBrowser;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;
    private int currentImageIndex = 0;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void ResetImageIndex()
    {
        currentImageIndex = 0;
    }

    public void OpenAndSaveImage(string sectionKey, System.Action onComplete)
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".png", ".jpg", ".jpeg"));
        FileBrowser.ShowLoadDialog(
            (paths) =>
            {
                if (paths.Length > 0)
                {
                    string imageKey = sectionKey + "_image_" + currentImageIndex;
                    DataManager.Instance.SaveAnswer(imageKey, paths[0]);
                    currentImageIndex++;
                }
                onComplete?.Invoke();
            },
            () => onComplete?.Invoke(), // annulation
            FileBrowser.PickMode.Files,
            false,
            null,
            null,
            "Choisir une image",
            "Sélectionner"
        );
    }

    public List<string> GetImagePaths(string sectionKey)
    {
        List<string> paths = new List<string>();
        int index = 0;

        while (true)
        {
            string imageKey = sectionKey + "_image_" + index;
            string path = DataManager.Instance.GetAnswerOrPlaceholder(imageKey, "");
            if (string.IsNullOrEmpty(path)) break;
            paths.Add(path);
            index++;
        }

        return paths;
    }
}