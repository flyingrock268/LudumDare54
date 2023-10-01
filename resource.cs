using Godot;
using System;

public partial class resource : Sprite2D
{

    public enum types {

        NONE,
        MINE,
        STONE,
        FORREST,
        WOOD,
        STICK,
        FIRE,
        HAMMER,
        IRONORE,
        IRON,
        TOOL,
        GOLDORE,
        GOLD,
        GEM,
        CROWN,
        PLANK,
        CHAIR,
        THRONE

    }

    [Export]
    public types type;
    public bool isActive = false;

    public override void _Ready()
    {

        updateTexture();

    }

    public void updateTexture() {

        if (type != types.NONE)
        {

            Texture = ImageTexture.CreateFromImage(Image.LoadFromFile("D:\\Godot projects\\LudumDare\\resources\\" + type.ToString() + ".png"));

        }
        else { 
        
            Texture = null;

        }
    
    }

    public void UpdateType(types type) { 
    
        this.type = type;
        updateTexture();
    
    }

}
