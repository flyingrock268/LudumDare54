using Godot;
using System;

public partial class StartMenu : Node2D
{

	Camera2D Camera;
	Button Play;
	Button Tutorial;
	Button Credits;
	Button Exit;

	//[Export]
	PackedScene playlevel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		Camera = GetNode<Camera2D>("Camera2D");
		Play = GetNode<Button>("PlayButton");
        Tutorial = GetNode<Button>("TutButton");
        Credits = GetNode<Button>("CreditsButton");
        Exit = GetNode<Button>("ExitButton");

        GetNode<Button>("ReturnButton").ButtonDown += OnReturn;
        GetNode<Button>("ReturnButton2").ButtonDown += OnReturn;

		Play.ButtonDown += OnPlay;
		Tutorial.ButtonDown += OnTutorial;
		Credits.ButtonDown += OnCredits;
		Exit.ButtonDown += OnExit;

	}

	public void OnPlay() {

		//GetTree().ChangeSceneToPacked(playlevel);
		GetTree().ChangeSceneToFile("res://main.tscn");

	}

	public void OnTutorial() { 
	
		Camera.Position = new Vector2 (512, 0);
	
	}

    public void OnCredits()
    {

        Camera.Position = new Vector2(1024, 0);

    }

    public void OnReturn() {

		Camera.Position = new Vector2(0, 0);
	
	}

	public void OnExit()
	{

		GetTree().Quit();

	}

}
