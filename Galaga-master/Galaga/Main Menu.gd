extends Control

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	Sound.get_node("Game Music").stop()
	Sound.get_node("Modified Game Music").stop()
	
	SoundVariables.playModifiedGame = true
	SoundVariables.playGame = true
	
	if SoundVariables.playMenu:
		print("playing main menu")
		Sound.get_node("Main Menu").play()
		SoundVariables.playMenu = false
	pass

func _on_OriginalGalagaButton_pressed():
	get_tree().change_scene("res://OriginalGalaga.tscn")
	pass # replace with function body

func _on_ModifiedGalagaButton_pressed():
	get_tree().change_scene("res://ModifiedGalaga.tscn")
	pass # replace with function body
  
func _on_ControlsButton_pressed():
	get_tree().change_scene("res://Controls.tscn")
	pass

func _on_Music_Credit_pressed():
	get_tree().change_scene("res://Credits.tscn")
	pass # replace with function body
