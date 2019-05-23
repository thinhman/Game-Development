extends KinematicBody2D

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
export var velocity = Vector2(0,0);

func _ready():
	set_process(true)
	pass

func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
	translate(velocity * delta)
	pass
	

	
