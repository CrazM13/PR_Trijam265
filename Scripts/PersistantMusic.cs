using Godot;
using System;

public partial class PersistantMusic : AudioStreamPlayer {

	private static float currentPosition = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		this.Seek(currentPosition);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		currentPosition = this.GetPlaybackPosition();
	}
}
