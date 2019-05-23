extends Node2D

export var enemyCount = 0

onready var player = weakref(get_node("/root/Root/Ship"))
onready var respawn_wait_timer = get_node("RespawnTimer")
onready var respawn_ship_timer = get_node("RespawnShipTimer")
onready var restart_timer = get_node("RestartTimer")
onready var text_label = get_node("/root/Root/GUI/Game_Contents/TextLabel")
onready var alien_scene_parent = get_node("/root/Root/aliens_scene")
onready var background_shader = get_node("/root/Root/Background")
onready var animator = get_node("/root/Root/AnimationPlayer")
onready var warningSign = get_node("/root/Root/GUI/Game_Contents/Warning")
onready var missileRect = get_node("/root/Root/GUI/BottomUI/Items/MissileIndicator/MissileRect")
onready var shieldRect = get_node("/root/Root/GUI/BottomUI/Items/ShieldIndicator/ShieldRect")

onready var GUI_Item = get_node("/root/Root/GUI")
const ship_scn = preload("res://Ship Modified.tscn")

var can_shoot = true
var respawn_location = Vector2()
var lives
var score
var level
var laser = false
var shield = false
var high_score
var won = false
var main_node
var shader_material

func _ready():
	# Get the shader from the background and modify its speed to 0.25 which is
	# slow but not as slow as the main menu speed
	shader_material = background_shader.get_material()
	shader_material.set_shader_param("speed",0.25)
	
	# Stop playing the main menu and allow the menu to be 
	# played if the game goes back to there
	Sound.get_node("Main Menu").stop()
	SoundVariables.playMenu = true
	
	if SoundVariables.playModifiedGame:
		Sound.get_node("Modified Game Music").play()
		SoundVariables.playModifiedGame = false
		
	# Try to make a new save file
	var save_game = File.new()
	# If it doesn't exist yet make a new file with 
	# the defaults given below
	if not save_game.file_exists("user://modifiedsavegame.save"):
		lives = 3
		score = 0
		level = 0
		high_score = 0
		laser = false
		shield = false
		respawn_location = player.position
	else:
		# File existed and the game is now loaded
		load_game()
		player.get_ref().position = respawn_location
		get_node("/root/Root/GUI").lost_life(lives)
	
	# Level logic as it goes further on
	if level > 0:
		update_enemies()
		
	# Hide the text and connect timers for respawning, shooting, and level reloading
	text_label.hide()
	warningSign.hide()
	main_node = get_node("../")
	enemyCount = get_node("/root/Root/aliens_scene").get_child_count()
	respawn_wait_timer.connect("timeout",self,"restart_shooting")
	respawn_ship_timer.connect("timeout",self,"respawn_ship")
	restart_timer.connect("timeout",self,"goto_main_menu")
	pass

func _process(delta):
	# If the player is alive, check to see if we display the item
	# on the bottom gui
	display_or_hide_texture_rects()
	
	# Do we have enemies on the map?
	if enemyCount == 0:
		# Put the text up and display the message below
		text_label.show()
		text_label.set_text("You Win!\nGet ready for next zone!")
		# If we havent won (which is always for each level load) increase the level number
		# and save game. Calling restart_level_... function checks to see where
		# to load to. 
		if !won:
			level += 1
			save_game()
			restart_level_or_main_menu()
		won = true
		# Play the animator if its not playing and show the jett trails
		if !animator.is_playing():
			player = weakref(get_node("/root/Root/Ship"))
			player.get_ref().showTrails()
			animator.play("LevelComplete")
	# If we are out of lives we lost and itll display that message on the screen
	if lives == 0:
		text_label.show()
		text_label.set_text("You lose!")
		won = false
	
	pass
	
func destroy_nodes():
	for i in main_node.get_children():
		i.queue_free()
		
# Stop the timer and update the high score if needed
# If we won go reload the same scene and if not go back
# to main menu and reload the file with the basics
func goto_main_menu():
	restart_timer.stop()
	#check if we got a high score
	if score > high_score:
		high_score = score
	if won:
		get_tree().reload_current_scene()
	else:
		get_tree().change_scene("res://Main Menu.tscn")
		lives = 3
		score = 0
		level = 0
		laser = false
		shield = false
		respawn_location = Vector2(235,685)
		save_game()
	
func stop_shooting_timer():
	can_shoot = false
	respawn_wait_timer.start()
	
func restart_shooting():
	if lives != 0:
		can_shoot = true
	else:
		can_shoot = false
	respawn_wait_timer.stop()

func delay_ship_spawn():
	respawn_ship_timer.start()
	
func respawn_ship():
	var ship = ship_scn.instance()
	ship.position = respawn_location
	get_parent().add_child(ship)
	respawn_ship_timer.stop()

func restart_level_or_main_menu():
	restart_timer.start()

# What the save data consists of
func save():
	var save_dict = {
		"High_Score" : score,
		"Previous_High_Score" : high_score,
		"Lives" : lives,
		"Level" : level,
		"Pos_x" : respawn_location.x,
		"Pos_y" : respawn_location.y,
		"HasLaser" : laser,
		"HasShield" : shield
	}
	return save_dict

# Save the game data to json
func save_game():
	print("Saved file")
	var save_game = File.new()
	save_game.open("user://modifiedsavegame.save", File.WRITE)
	var save_nodes = get_tree().get_nodes_in_group("Persist")
	for i in save_nodes:
		var node_data = i.call("save");
		save_game.store_line(to_json(node_data))
	save_game.close()

func load_game():
	print("Load file")
	var save_game = File.new()
	if not save_game.file_exists("user://modifiedsavegame.save"):
	    return # Error! We don't have a save to load.
		
	save_game.open("user://modifiedsavegame.save", File.READ)
	while not save_game.eof_reached():
		var current_line = parse_json(save_game.get_line())
		print(current_line)
		# Firstly, we need to create the object and add it to the tree and set its position.
		score = int(current_line["High_Score"])
		high_score = int(current_line["Previous_High_Score"])
		lives = int(current_line["Lives"])
		level = int(current_line["Level"])
		respawn_location = Vector2(current_line["Pos_x"],current_line["Pos_y"])
		laser = bool(current_line["HasLaser"])
		shield = bool(current_line["HasShield"])
		if laser:
			get_node("/root/Root/Ship").itemLaser = true
		if shield:
			get_node("/root/Root/Ship").item = true
		for i in current_line.keys():
			if i == "High_Score" or i == "Lives" or i == "Level" or i == "Previous_High_Score" or i == "Pos_x" or i == "Pos_y" or i == "HasLaser" or i == "HasShield":
				continue
		break
	save_game.close()

#if they force close it resets the game to the initial values
func _notification(what):
	if what == MainLoop.NOTIFICATION_WM_QUIT_REQUEST:
		lives = 3
		if score > high_score:
			high_score = score
		score = 0
		level = 0
		laser = false
		shield = false
		respawn_location = Vector2(235,685)
		save_game()
		get_tree().quit() # default behavior
		
# Time to make the game a bit more exciting RNG wise and decrease there
# cooldowns and reload time by a percentage each level
func update_enemies():
	print(alien_scene_parent.get_tree().get_nodes_in_group("Alien").size())
	for child in alien_scene_parent.get_tree().get_nodes_in_group("Alien"):
		#take 10% off the random firing rate for now
		child.random_shoot_var = child.random_shoot_var - (level * (child.random_shoot_var * .10))
		#take 5% off the reload time for now
		child.reload_time = child.reload_time - ((level * child.reload_time * 0.05))

# Level cleared logic
func on_level_clear():
	shader_material.set_shader_param("speed",1.0)
	
func display_or_hide_texture_rects():
	if player.get_ref():
		# Check if we have a shield
		if player.get_ref().shield == true:
			shieldRect.visible = true
		else:
			shieldRect.visible = false
		
		# Check if we got a missile
		if player.get_ref().missile == true:
			missileRect.visible = true
		else:
			missileRect.visible = false