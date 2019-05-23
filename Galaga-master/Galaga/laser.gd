extends Area2D

# class member variables go here, for example:
# var a = 2
# var b = "textvar"

export var velocity = Vector2();
onready var game_manager = get_node("/root/Root/GameManager")
const ship_explosion = preload("res://ShipDeath.tscn")
var shooter = String()

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	Sound.get_node("Laser Shot").play()
	set_process(true)
	$notifier.connect("screen_exited", self, "off_screen")
	pass

func _process(delta):
#	#  every frame. Delta is time since last frame.
#	# Update game logic here.
	translate(velocity * delta)
	pass 

func off_screen():
	queue_free()

func _on_laser_body_entered(body):
	if (body.name != shooter and body.name == "alien"):
		body.get_node("../../../").queue_free()
		queue_free()
		game_manager.score += body.points
		game_manager.enemyCount -= 1
	elif velocity == Vector2(0, 600) and body.name == "Shield":
		queue_free()
		return
	elif(body.name != shooter and body.name == "Ship"):
		var explosion = ship_explosion.instance()
		explosion.position = body.position
		get_parent().add_child(explosion)
		explosion.play()
		queue_free()
		game_manager.stop_shooting_timer()
		game_manager.lives -= 1
		if game_manager.lives == 0:
			SoundVariables.playGame = true
			SoundVariables.playModifiedGame = true
			game_manager.restart_level_or_main_menu()
			body.queue_free()
		else:
			body.queue_free()
			game_manager.delay_ship_spawn()
			get_node("/root/Root/GUI").lost_life(game_manager.lives)
	pass

