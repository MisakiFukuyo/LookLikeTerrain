using UnityEngine;
using System.Collections;

public class ChilD : MonoBehaviour {
    public int[] pens;
    public int penSize;
    public int maxSpeed;
    public int x, y;
    public float power;

    int canvasX,canvasY;
    int[,] canvas;

    public void Init(int canvasX, int canvasY, int[,] canvas)
    {
        this.canvasX = canvasX;
        this.canvasY = canvasY;
        this.canvas = canvas;
    }

    bool IsInCanvas(int kx,int ky)
    {
        return kx >= 0 && kx < canvasX && ky >= 0 && ky < canvasY; 
    }

    void DrawMicro(bool redraw,int p)
    {
        for (int i = -penSize; i < penSize; i++)
        {
            for (int j = -penSize; j < penSize; j++)
            {
                if (IsInCanvas(x + j, y + i) && (redraw || canvas[x + j, y + i] == 0))
                {
                    canvas[x + j, y + i] = p;
                }
            }
        }
    }


    void DrawMacro(float c, float r, bool redraw)
    {
        c = Mathf.Deg2Rad * c;
        float cs = Mathf.Cos(c);
        float sn = Mathf.Sin(c);
        for(float d = 0f;d<r;d+=2f){
            x += (int)(d * cs);
            y += (int)(d * sn);
            int p = pens[Random.Range(0, pens.Length)];
            DrawMicro(redraw,p);
        }
    }

    public void Draw()
    {
        while (true)
        {
            float dice = Random.Range(0f, 100f);
            float c = Random.Range(0f, 360f);
            float r = Random.Range(1f, maxSpeed);
            if (IsInCanvas((int)(x + r * Mathf.Cos(Mathf.Deg2Rad * c)), (int)(y + r * Mathf.Sin(Mathf.Deg2Rad * c))))
            {
                DrawMacro(c, r, power > dice);
                break;
            }
            else
            {
                continue;
            }
        }
    }
}
