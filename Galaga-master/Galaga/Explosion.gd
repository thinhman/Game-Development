extends Area2D

# Get the game manager
onready var game_manager = get_node("/root/Root/GameManager")

func _ready():
	# Connect the body_entered signal
	# $ExplosionNotifier.connect("body_entered", self, "_on_Explosion_body_entered")
	# Start the timer
	get_node("ExplosionTimer").start()
	pass

func _process(delta):
#	# Check the explosion timer
	if get_node("ExplosionTimer").get_time_left() == 0:
		queue_free()
	pass

# Signal for when a body is entered
func _on_Explosion_body_entered(body):
	# Kill the parent, add points, and update the manager enemy count
	if(body.name == "alien"):
		body.get_node("../../../").queue_free()
		#queue_free()
		game_manager.score += body.points
		game_manager.enemyCount -= 1
	pass # replace with function body
