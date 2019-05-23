extends Path2D

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
onready var follow = get_node("PathFollow2D")
onready var game_manager = get_node("/root/Root/GameManager")

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	set_process(true)
	pass

func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
	follow.set_offset(follow.get_offset() + 200 * delta)
	pass
