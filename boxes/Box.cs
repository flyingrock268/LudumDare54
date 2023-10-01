using Godot;
using System;

public partial class Box : Area2D
{

	Player player;
	resource resource;
	Timer timer;

    public Box[] neighbors = new Box[8];

    RandomNumberGenerator rng;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

        rng = new RandomNumberGenerator();
		player = GetTree().Root.GetNode<Player>("Main");
		resource = GetNode<resource>("resource");
        GetNode<Node2D>("Sprite2D").Rotation = Rotation;
        Rotation = 0;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public resource GetResource() { 
    
        return resource;

    }

    public void SetResource(resource resource) {

        this.resource.UpdateType(resource.type);

    }

    public void SetResource(resource.types resource)
    {

        this.resource.UpdateType(resource);

    }

    public void OnClick()
    {

        if (resource.type == resource.types.WOOD)
        {

            if (neighbors[2] != null && neighbors[2].resource.type == resource.types.WOOD)
            {

                resource.UpdateType(resource.types.FORREST);
                neighbors[2].resource.UpdateType(resource.types.NONE);

            }

            else if (neighbors[6] != null && neighbors[6].resource.type == resource.types.WOOD)
            {

                resource.UpdateType(resource.types.FORREST);
                neighbors[6].resource.UpdateType(resource.types.NONE);

            }

            else
            {

                resource.UpdateType(resource.types.STICK);

            }

        }

        else if (resource.type == resource.types.STONE)
        {

            if (neighbors[2] != null && neighbors[2].resource.type == resource.types.STONE)
            {

                resource.UpdateType(resource.types.MINE);
                neighbors[2].resource.UpdateType(resource.types.NONE);

            }

            else if (neighbors[6] != null && neighbors[6].resource.type == resource.types.STONE)
            {

                resource.UpdateType(resource.types.MINE);
                neighbors[6].resource.UpdateType(resource.types.NONE);

            }

        }

        else if (resource.type == resource.types.FORREST)
        {

            if (neighbors[2] != null && neighbors[2].GetResource().type == resource.types.NONE)
            {

                neighbors[2].SetResource(resource.types.WOOD);

            }

        }

        else if (resource.type == resource.types.MINE)
        {
            if (neighbors[0] == null || neighbors[0].GetResource().type != resource.types.TOOL)
            {

                if (neighbors[2] != null && neighbors[2].GetResource().type == resource.types.NONE)
                {

                    float temp = rng.Randf();

                    if (temp > .25)
                    {

                        neighbors[2].SetResource(resource.types.STONE);

                    }

                    else
                    {

                        neighbors[2].SetResource(resource.types.IRONORE);

                    }

                }

            }

            else
            {

                if (neighbors[2] != null && neighbors[2].GetResource().type == resource.types.NONE)
                {

                    neighbors[2].SetResource(resource.types.GOLDORE);

                }
            }

        }

        else if (resource.type == resource.types.STICK)
        {

            if (neighbors[2] != null && neighbors[2].GetResource().type == resource.types.STICK)
            {

                SetResource(resource.types.FIRE);
                neighbors[2].SetResource(resource.types.NONE);

            }

            else if (neighbors[6] != null && neighbors[6].GetResource().type == resource.types.STICK)
            {

                SetResource(resource.types.FIRE);
                neighbors[6].SetResource(resource.types.NONE);

            }

            else if (neighbors[0] != null && neighbors[0].GetResource().type == resource.types.STONE)
            {

                SetResource(resource.types.HAMMER);
                neighbors[0].SetResource(resource.types.NONE);

            }

            else if (neighbors[0] != null && neighbors[0].GetResource().type == resource.types.IRON)
            {

                SetResource(resource.types.TOOL);
                neighbors[0].SetResource(resource.types.NONE);

            }

        }

        else if (resource.type == resource.types.HAMMER)
        {

            if (neighbors[4] != null)
            {

                if (neighbors[4].GetResource().type == resource.types.STONE)
                {

                    float temp = rng.Randf();

                    if (temp > .9f)
                    {

                        neighbors[4].SetResource(resource.types.GEM);

                    }

                    else
                    {

                        neighbors[4].SetResource(resource.types.NONE);

                    }

                }

                else
                {

                    neighbors[4].SetResource(resource.types.NONE);

                }

            }

        }

        else if (resource.type == resource.types.IRONORE)
        {

            if (neighbors[4] != null && neighbors[4].GetResource().type == resource.types.FIRE)
            {

                SetResource(resource.types.IRON);
                neighbors[4].SetResource(resource.types.NONE);

            }

        }

        else if (resource.type == resource.types.GOLDORE)
        {

            if (neighbors[4] != null && neighbors[4].GetResource().type == resource.types.FIRE)
            {

                SetResource(resource.types.GOLD);
                neighbors[4].SetResource(resource.types.NONE);


            }

        }

        else if (resource.type == resource.types.TOOL)
        {

            if (neighbors[2] != null && neighbors[2].GetResource().type == resource.types.WOOD)
            {

                neighbors[2].SetResource(resource.types.PLANK);

            }

        }

        else if (resource.type == resource.types.GEM)
        {

            if (neighbors[0] != null && neighbors[0].GetResource().type == resource.types.GOLD)
            {

                resource.UpdateType(resource.types.CROWN);
                neighbors[0].resource.UpdateType(resource.types.NONE);

            }

        }

        else if (resource.type == resource.types.PLANK)
        {

            if (neighbors[2] != null && neighbors[2].resource.type == resource.types.PLANK)
            {

                resource.UpdateType(resource.types.CHAIR);
                neighbors[2].resource.UpdateType(resource.types.NONE);

            }

            else if (neighbors[6] != null && neighbors[6].resource.type == resource.types.PLANK)
            {

                resource.UpdateType(resource.types.CHAIR);
                neighbors[6].resource.UpdateType(resource.types.NONE);

            }

        }

        else if (resource.type == resource.types.CHAIR)
        {

            if (neighbors[0] != null && neighbors[0].GetResource().type == resource.types.GOLD)
            {

                resource.UpdateType(resource.types.THRONE);
                neighbors[0].resource.UpdateType(resource.types.NONE);

            }

        }

        else if (resource.type == resource.types.THRONE) {

            if (neighbors[2] != null && neighbors[2].GetResource().type == resource.types.CROWN) {

                GetTree().Root.GetNode<Player>("Main").OnWin();
            
            }

            if (neighbors[6] != null && neighbors[6].GetResource().type == resource.types.CROWN)
            {

                GetTree().Root.GetNode<Player>("Main").OnWin();

            }

        }

        else if (resource.type == resource.types.CROWN)
        {

            if (neighbors[2] != null && neighbors[2].GetResource().type == resource.types.THRONE)
            {

                GetTree().Root.GetNode<Player>("Main").OnWin();

            }

            if (neighbors[6] != null && neighbors[6].GetResource().type == resource.types.THRONE)
            {

                GetTree().Root.GetNode<Player>("Main").OnWin();

            }

        }
    }

}
