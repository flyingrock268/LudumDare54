using Godot;
using Godot;
using System;

public partial class Player : Node2D
{

	[Export]
	Box activeBox;
    [Export]
    PackedScene mainMenu;

	Box[,] boxes;

    int size = 3;
	Vector2I playerPos = new Vector2I();
	Vector2I Active = new Vector2I();

	Sprite2D player;

    RandomNumberGenerator rng = new RandomNumberGenerator();

    int totalUsed = 0;
    int totalMoved = 0;
    int totalMisplays = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

        boxes = new Box[size,size];

        //get boxes
		for (int i = 0; i < size; i++)
		{

			for (int j = 0; j < size; j++) {

                boxes[i, j] = GetNode("Boxes").GetChild<Box>(size * i + j);

            }

		}

        //set neighbors
        for (int i = 0; i < size; i++)
        {

            for (int j = 0; j < size; j++)
            {

                if (i != 0)
                {

                    boxes[i, j].neighbors[0] = boxes[i - 1, j];

                    if (j < size-1)
                    {

                        boxes[i, j].neighbors[1] = boxes[i - 1, j + 1];


                    }

                    if (j > 0)
                    {

                        boxes[i, j].neighbors[7] = boxes[i - 1, j - 1];

                    }


                }

                if (i != size - 1)
                {

                    boxes[i, j].neighbors[4] = boxes[i + 1, j];

                    if (j < size - 1)
                    {

                        boxes[i, j].neighbors[3] = boxes[i + 1, j + 1];


                    }

                    if (j > 0)
                    {

                        boxes[i, j].neighbors[5] = boxes[i + 1, j - 1];

                    }

                }

                if (j < size - 1)
                {

                    boxes[i, j].neighbors[2] = boxes[i, j + 1];


                }

                if (j > 0)
                {

                    boxes[i, j].neighbors[6] = boxes[i, j - 1];

                }

            }

        }

        //get player
        player = GetNode("Boxes").GetChild<Sprite2D>(size * size);

        GetNode<Button>("SelectButton").ButtonDown += OnSelect;
        GetNode<Button>("MoveButton").ButtonDown += OnMove;
        GetNode<Button>("TrashButton").ButtonDown += OnTrash;
        GetNode<Button>("ResetButton").ButtonDown += resetBoxes;
        GetNode<Control>("Container").Visible = false;


        resetBoxes();

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("Use")) {

            OnSelect();
            getBox(playerPos).OnClick();
            totalUsed++;

            GetNode<AudioStreamPlayer>("UseSound").Play();


            if (rng.Randf() * 1000 < totalMoved + totalUsed)
            {

                totalMisplays++;

            }

            //OnWin();

        }

		if (Input.IsActionJustPressed("MoveLeft")) {

			playerPos.X = Math.Clamp(playerPos.X-1, 0, size - 1);
		
		}
        if (Input.IsActionJustPressed("MoveRight"))
        {

            playerPos.X = Math.Clamp(playerPos.X + 1, 0, size - 1);

        }
        if (Input.IsActionJustPressed("MoveUp"))
        {

            playerPos.Y = Math.Clamp(playerPos.Y - 1, 0, size - 1);

        }
        if (Input.IsActionJustPressed("MoveDown"))
        {

            playerPos.Y = Math.Clamp(playerPos.Y + 1, 0, size - 1);

        }

		player.Position = playerPos * 64 - Position;

    }

    public Box getBox(Vector2I pos) { 
    
        return boxes[pos.Y, pos.X];
    
    }

    public void resetBoxes() {

        totalUsed = 0;
        totalMoved = 0;
        totalMisplays = 0;

        GetNode<Node2D>("Boxes").Visible = true;
        GetNode<Button>("SelectButton").Visible = true;
        GetNode<Button>("MoveButton").Visible = true;
        GetNode<Button>("TrashButton").Visible = true;
        GetNode<Button>("ResetButton").Visible = true;
        GetNode<Control>("Container").Visible = false;


        for (int i = 0; i < size; i++) {

            for (int j = 0; j < size; j++)
            {
                boxes[i, j].SetResource(resource.types.NONE);
            
            }
        
        }

        boxes[0,0].SetResource(resource.types.WOOD);
        boxes[0,1].SetResource(resource.types.WOOD);

        boxes[1, 0].SetResource(resource.types.STONE);
        boxes[1, 1].SetResource(resource.types.STONE);

    }

    public void OnSelect() {

        boxes[Active.Y, Active.X].Modulate = new Color(1,1,1);

        Active = playerPos;

        boxes[Active.Y, Active.X].Modulate = new Color(1, 1, .5f);

        GetNode<AudioStreamPlayer>("UseSound").Play();

    }

    public void OnMove() {

        resource.types temp = getBox(Active).GetResource().type;
        getBox(Active).SetResource(getBox(playerPos).GetResource());
        getBox(playerPos).SetResource(temp);

        totalMoved++;
        if (rng.Randf() * 1000 < totalMoved + totalUsed)
        {

            totalMisplays++;

        }

        GetNode<AudioStreamPlayer>("MoveSound").Play();

    }

    public void OnTrash() {

        getBox(playerPos).SetResource(resource.types.NONE);
        GetNode<AudioStreamPlayer>("TrashSound").Play();


    }

    public void OnWin() {

        GetNode<Node2D>("Boxes").Visible = false;
        GetNode<Button>("SelectButton").Visible = false;
        GetNode<Button>("MoveButton").Visible = false;
        GetNode<Button>("TrashButton").Visible = false;
        GetNode<Button>("ResetButton").Visible = false;
        GetNode<Control>("Container").Visible = true;

        GetNode<Label>("Container/UsesLabel").Text = "Uses: " + totalUsed;
        GetNode<Label>("Container/MovesLabel").Text = "Moves: " + totalMoved;
        GetNode<Label>("Container/MisplayLabel").Text = "Misplays: " + totalMisplays;

    }

    public void OnExit() {

        GetTree().Quit();
    
    }

    public void OnMainMenu() {

        GetTree().ChangeSceneToFile("res://MainMenu.tscn");

    }
}
