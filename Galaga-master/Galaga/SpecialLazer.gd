extends Area2D

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
export var vel = Vector2();
const Explosion_scn = preload("res://Explosion.tscn")

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	#$notifier.connect("body_entered", self, "_on_SpecialLaser_body_entered")
	#$Explosion.get_node("CollisionShape2D").disabled = true
	#$Explosion.visible = false
	set_process(true)
	pass

func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
	#if get_node("ExplosionTimer").get_time_left() == 0:
		#queue_free()
	vel = Vector2(0,-300)
	translate(vel * delta)
	pass
	
func start_at(pos):
	vel.x = pos.x
	vel.y = pos.y - 40
	set_position(vel)
	pass

func _on_SpecialLaser_body_entered(body):
	if(body.name == "alien"):
		#body.get_node("../../../").queue_free()
		#get_node("CollisionShape2D").disabled = true
		#visible = false
		#$Explosion.get_node("CollisionShape2D").disabled = false
		#$Explosion.visible = true
		var explosion = Explosion_scn.instance()
		explosion.position = get_position()
		Sound.get_node("Ship Explosion").play()
		get_parent().add_child(explosion)
		queue_free()
		#game_manager.score += body.points
		#game_manager.enemyCount -= 1
	pass # replace with function body
