extends Area2D

export var velocity = Vector2(0, 100)

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	# $notifier.connect("body_entered", self, "_on_LaserItem_body_entered")
	pass

func _process(delta):
	set_position(get_position() + velocity * delta)
	pass

func _on_LaserItem_body_entered(body):
	if(body.name == "Ship"):
		body.getLaser()
		queue_free()
		return
	pass

func _on_notifier_screen_exited():
	queue_free()
	pass # replace with function body
