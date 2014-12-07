using UnityEngine;
using System.Collections;

public class ChilDraw : MonoBehaviour {
    public int canvasX;
    public int canvasY;
    public ChilD[] children;

    int[,] canvas;

    public Renderer targetRenderer;

    Texture2D canvasResult;

    public bool isWait = true;
    public float wait = 0.05f;

    public bool finish;

    public int[] colorTable;
    public Color[] colors;

    bool IsDrawed()
    {
        for (int x = 0; x < canvasX; x++)
        {
            for (int y = 0; y < canvasY; y++)
            {
                if (canvas[x, y] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void TextureDraw()
    {
        for (int x = 0; x < canvasX;x++ )
        {
            for (int y = 0; y < canvasY; y++)
            {
                int k = 0;
                foreach (int c in colorTable)
                {
                    if (canvas[x, y] == c)
                    {
                        canvasResult.SetPixel(x,y,colors[k]);
                        break;
                    }
                    k++;
                }
            }
        }
        canvasResult.Apply();
    }

    IEnumerator ExecDraw()
    {
        while (!finish)
        {
            foreach (var ch in children)
            {
                ch.Draw();
            }
            finish = IsDrawed();
            TextureDraw();
            if (isWait)
            {
                yield return new WaitForSeconds(wait);
            }
        }
        yield return null;
    }

	void Start () {
        canvas = new int[canvasX, canvasY];
        canvasResult = new Texture2D(canvasX, canvasY);
        TextureDraw();
        targetRenderer.material.mainTexture = canvasResult;
        foreach (var ch in children)
        {
            ch.Init(canvasX, canvasY,canvas);
        }
        StartCoroutine(ExecDraw());
	}
	
    void Update () {
	
	}
}
