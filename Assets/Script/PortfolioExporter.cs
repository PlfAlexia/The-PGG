using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PortfolioExporter : MonoBehaviour
{
    public static PortfolioExporter Instance;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void OpenSaveFolder()
    {
        Application.OpenURL("file://" + Application.persistentDataPath);
    }

    public void ExportToHTML()
    {
        string html = GenerateHTML();
        string exportPath = Path.Combine(Application.persistentDataPath, "portfolio.html");
        File.WriteAllText(exportPath, html, System.Text.Encoding.UTF8);

        // Ouvre directement le fichier dans le navigateur
        Application.OpenURL("file://" + exportPath);
    }

    private string GenerateHTML()
    {
        List<string> keys = DataManager.Instance.GetAllQuestionKeys();

        string sections = "";
        foreach (string key in keys)
        {
            string title = DataManager.Instance.GetAnswerOrPlaceholder(key + "_title", key);
            string answer = DataManager.Instance.GetAnswerOrPlaceholder(key, "Non renseigné");
            string  imagesHTML = "";
            List<string> imagePaths = ImageManager.Instance.GetImagePaths(key);
            
            foreach (string path in imagePaths)
            {
                imagesHTML += $@"
                <div class='image-container'>
                    <img src='file:///{path.Replace("\\", "/")}' 
                        alt='Illustration' 
                        class='portfolio-image'/>
                </div>";
            }


            sections += $@"
            <section class='section'>
                <h2>{title}</h2>
                <p>{answer}</p>
            </section>";
        }

        // Template HTML complet
        return $@"<!DOCTYPE html>
<html lang='fr'>

<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Mon Portfolio</title>

    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}

        body {{
            font-family: 'Segoe UI', sans-serif;
            background: #0f0f0f;
            color: #ffffff;
            padding: 40px 20px;
        }}

        .container {{
            max-width: 800px;
            margin: 0 auto;
        }}

        header {{
            text-align: center;
            margin-bottom: 60px;
            padding-bottom: 30px;
            border-bottom: 1px solid #333;
        }}

        header h1 {{
            font-size: 48px;
            font-weight: 700;
            letter-spacing: 2px;
            margin-bottom: 10px;
        }}

        header p {{
            color: #888;
            font-size: 16px;
        }}

        .section {{
            margin-bottom: 50px;
            padding: 30px;
            background: #1a1a1a;
            border-radius: 12px;
            border-left: 4px solid #ffffff;
        }}

        .section h2 {{
            margin-bottom: 15px;
            font-size: 24px;
            font-weight: 600;
        }}

        .section p {{
            color: #cccccc;
            line-height: 1.8;
            font-size: 16px;
        }}

        .portfolio-image {{
            max-width: 100%;
            border-radius: 8px;
            margin-top: 20px;
        }}

        .image-container {{
            margin-top: 15px;
        }}

        footer {{
            text-align: center;
            margin-top: 60px;
            color: #444;
            font-size: 14px;
        }}
    </style>

</head>

<body>
    <div class='container'>
        <header>
            <h1>Mon Portfolio</h1>
            <p>Généré depuis The PGG</p>
        </header>

        {sections}

        <footer>
            <p>Créé avec The PGG</p>
        </footer>
    </div>
</body>
</html>";
    }
}