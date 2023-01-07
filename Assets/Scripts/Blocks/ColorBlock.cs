using UnityEngine;

public class ColorBlock : Block
{
    public override void Start()
    {
         base.Start();
    }
    
    public void Entered(Color c)
    {
          if (c == currentColor) return;
          SetColor(c);
    }
}
