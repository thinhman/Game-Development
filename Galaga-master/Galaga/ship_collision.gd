extends Area2D

onready var game_manager = get_node("/root/Root/GameManager")
# class member variables go here, for example:
# var a = 2
# var b = "textvar"

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	connect("body_entered", self, "_on_body_entered")
	pass

func _on_body_entered(body):
	if(body.name == "Ship"):
		queue_free()
		game_manager.stop_shooting_timer()
		game_manager.lives -= 1
		if game_manager.lives == 0:
			game_manager.restart_level_or_main_menu()
			body.queue_free()
		else:
			body.queue_free()
			game_manager.delay_ship_spawn()
			get_node("/root/Root/GUI").lost_life(game_manager.lives)
	elif body.name == "Shield":
		get_node("../../../").queue_free()
		game_manager.score += get_node("../alien").points

		game_manager.enemyCount -= 1
	pass

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass
