using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AutomaticUIAnchoringEditor : Editor
{
    static AutomaticUIAnchoringEditor()
    {
        if (!EditorPrefs.HasKey("AutomaticUIAnchoringEditor_isFirstOperation"))
            EditorPrefs.SetBool("AutomaticUIAnchoringEditor_isFirstOperation", true);

        if (!EditorPrefs.HasKey("AutomaticUIAnchoringEditor_showPopup"))
            EditorPrefs.SetBool("AutomaticUIAnchoringEditor_showPopup", true);

  //      if (EditorPrefs.GetBool("AutomaticUIAnchoringEditor_showPopup"))
    //        EditorApplication.quitting += delegate { EditorPrefs.SetBool("AutomaticUIAnchoringEditor_isFirstOperation", true); };
    }

    [MenuItem("Tools/AndrewRise1998/Automatic UI Anchoring/Automatically Anchor All UI Objects")]
    private static void AutomaticallyAnchorAllUIObjects()
    {
        RectTransform[] rectTransforms = Resources.FindObjectsOfTypeAll(typeof(RectTransform)) as RectTransform[];
        for (int i = 0; i < rectTransforms.Length; i++)
            Anchor(rectTransforms[i]);

        DisplayAboutDialog();
    }

    [MenuItem("Tools/AndrewRise1998/Automatic UI Anchoring/Automatically Anchor Selected UI Objects")]
    private static void AutomaticallyAnchorSelectedUIObjects()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            RectTransform rectTransform = Selection.gameObjects[i].GetComponent<RectTransform>();
            if (rectTransform)
                Anchor(rectTransform);
        }

        DisplayAboutDialog();
    }

    private static void Anchor(RectTransform rectTransform)
    {
        if (!rectTransform.transform.parent)
            return;
		UnityEngine.UI.Image ima = rectTransform.GetComponent<UnityEngine.UI.Image>();

			Rect parentRect = rectTransform.transform.parent.GetComponent<RectTransform>().rect;
			UnityEngine.UI.Image parentImage = rectTransform.transform.parent.GetComponent<UnityEngine.UI.Image>();
			if (parentImage != null)
			{
				if(parentImage.sprite != null)
				{
				var size = parentImage.sprite == null ? Vector2.zero : new Vector2(parentImage.sprite.rect.width, parentImage.sprite.rect.height);
				//parentRect = parentImage.sprite.rect;
				Rect r = parentImage.GetPixelAdjustedRect();
				if (size.sqrMagnitude > 0.0f)
				{
					var spriteRatio = size.x / size.y;
					
					var rectRatio = r.width / r.height;

					if (spriteRatio > rectRatio)
					{
						var oldHeight = r.height;
						r.height = r.width * (1.0f / spriteRatio);
						r.y += (oldHeight - r.height) * ((RectTransform)(rectTransform.transform.parent)).pivot.y;
					}
					else
					{
						var oldWidth = r.width;
						r.width = r.height * spriteRatio;
						r.x += (oldWidth - r.width) * ((RectTransform)(rectTransform.transform.parent)).pivot.x;
					}
					//parentRect = r;
				}
				Debug.Log("Parent Image");
				}
			}
			rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x + (rectTransform.offsetMin.x / parentRect.width), rectTransform.anchorMin.y + (rectTransform.offsetMin.y / parentRect.height));
			rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x + (rectTransform.offsetMax.x / parentRect.width), rectTransform.anchorMax.y + (rectTransform.offsetMax.y / parentRect.height));
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
			rectTransform.pivot = new Vector2(0.5f, 0.5f);
			rectTransform.pivot = new Vector2(0.5f, 0.5f);


    }

    [MenuItem("Tools/AndrewRise1998/Automatic UI Anchoring/About")]
    private static void About()
    {
        DisplayAboutDialog(true);
    }

    private static void DisplayAboutDialog(bool force = false)
    {
        if (force || EditorPrefs.GetBool("AutomaticUIAnchoringEditor_isFirstOperation"))
        {
            string cancelText = "Ok Don't Show Me This Again";
            if (force)
                cancelText = string.Empty;

            if (!EditorUtility.DisplayDialog("About Automatic UI Anchoring",
                "Thank you for using the Automatic UI Anchoring extension for Unity created by:"
                + System.Environment.NewLine +
                "Andrew Riseborough"
                + System.Environment.NewLine + System.Environment.NewLine +
                "If you would like to check out some of the other open-source awesome tools, scripts and side projects I've made you can find them on my GitHub at: https://github.com/AndrewRise1998"
                + System.Environment.NewLine + System.Environment.NewLine +
                "I hope you found this tool useful and it saved you some time. If it did and you would like to give me something for my time you can donate using any of the following methods:"
                + System.Environment.NewLine + System.Environment.NewLine +
                "PayPal:"
                + System.Environment.NewLine +
                "https://www.paypal.me/AndrewRise1998"
                + System.Environment.NewLine + System.Environment.NewLine +
                "BTC:"
                + System.Environment.NewLine +
                "3CVBKMEzJPJ6QWr2UVkbhZ1Xf9CsjtfoQr"
                + System.Environment.NewLine + System.Environment.NewLine +
                "BCH:"
                + System.Environment.NewLine +
                "qr7qft02dqqc6d90elpj87pv9plh3vy23ymfy4w89e"
                + System.Environment.NewLine + System.Environment.NewLine +
                "ETH:"
                + System.Environment.NewLine +
                "0x57e1390EC5BD3398dCC24eBA497F998288A1d474"
                + System.Environment.NewLine + System.Environment.NewLine +
                "ETC:"
                + System.Environment.NewLine +
                "0x3DB558385EAB37660c7CD974550CFd5d5B321b0a"
                + System.Environment.NewLine + System.Environment.NewLine +
                "LTC:"
                + System.Environment.NewLine +
                "MEqSEtCezQvy7duKmPpRj8vpQRm3qeRcjv"
                + System.Environment.NewLine + System.Environment.NewLine +
                "ZRX:"
                +System.Environment.NewLine +
                "0x890BDC772e9e43cDE33E06E8dEA86eE2c3900a2d",
                "Ok",
                cancelText)
            )
                EditorPrefs.SetBool("AutomaticUIAnchoringEditor_showPopup", false);
            
            if(!force)
                EditorPrefs.SetBool("AutomaticUIAnchoringEditor_isFirstOperation", false);
        }
    }
}
