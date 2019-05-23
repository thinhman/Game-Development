extends KinematicBody2D

#TODO:: Boss fight and then fix the drop's from the variance 
# and then get attack patterns to work 

export var horizontalSpeed = 200;
var texture_size;
var screen_size;
export var reload_time = 0.5;
var reload_timer = 0;

# Objects that are powerups for the ship
var shield = false
var missile = false
var ammo = false

# Preload scenes that you will use to instance variables
const laser_scn = preload("res://laser.tscn")
const SpecialLaser_scn = preload("res://SpecialLazer.tscn")

# Nodes that will be used 
onready var ammo_timer = get_node("AmmoTimer")
onready var animator = get_node("AnimationPlayer")
onready var leftTrail = get_node("LeftTrail")
onready var rightTrail = get_node("RightTrail")

func _ready():
	no_cooldown_shooting()
	# size of the sprite texture
	texture_size = $Sprite.texture.get_size()
	# gets the size of the screen to find the boundaries
	screen_size = get_viewport().get_visible_rect().size
	
	# hide the ship trails
	hideTrails()
	
	# attach a thing to check if the user is hitting the fire button
	$Shield.get_node("CollisionShape2D").disabled = true
	$Shield.visible = false
	ammo_timer.connect("timeout",self,"reset_powerups")
	pass

func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
	var velocity = Vector2(0,0)
	if !animator.is_playing():
		process_movement(velocity,delta)
		shoot_laser()
		shoot_SpecialLaser()
		reload_timer -= delta
		if $Shield.get_node("SheildDuration").get_time_left() == 0:
			$Shield.get_node("CollisionShape2D").disabled = true
			$Shield.visible = false
		if $AmmoTimer.get_time_left() == 0:
			ammo = false
	pass
	
func process_movement(velocity,delta):
	if Input.is_action_pressed("ui_left") and position.x - texture_size.x/2.0 >= 0:
		velocity.x = -horizontalSpeed;
	elif Input.is_action_pressed("ui_right") and position.x + texture_size.x/2.0 <= screen_size.x:
		velocity.x = horizontalSpeed;
	elif Input.is_key_pressed(KEY_X) and shield == true:
		$Shield.get_node("CollisionShape2D").disabled = false
		$Shield.visible = true
		$Shield.get_node("SheildDuration").start()
		shield = false
	move_and_collide(velocity * delta);
	
func shoot_laser():
	var laser = laser_scn.instance()
	if Input.is_action_pressed("shoot") and reload_timer <= 0.0:
		# Magic numbers that just seemed to work idk
		laser.position = Vector2(position.x - 5, position.y - (texture_size.y / 4.0))
		laser.shooter = name
		reload_timer = reload_time
		get_parent().add_child(laser)

func shoot_SpecialLaser():
	var laser = SpecialLaser_scn.instance()
	if Input.is_key_pressed(KEY_SPACE) and missile == true:
		laser.start_at(get_position())
		missile = false
		get_parent().add_child(laser)
func no_cooldown_shooting():
	reload_time = 0.1
	
func reset_powerups():
	ammo_timer.stop()
	reload_time = 0.5

func getItem():
	shield = true
func getLaser():
	missile = true
func getAmmo():
	ammo = true
	no_cooldown_shooting()
	ammo_timer.start()
	
func hideTrails():
	#hides the left trail used for the animation
	leftTrail.set_process(false)
	leftTrail.hide()
	
	#hides the right trail used for the animation
	rightTrail.set_process(false)
	rightTrail.hide()

func showTrails():
	#hides the left trail used for the animation
	leftTrail.set_process(true)
	leftTrail.show()
	
	#hides the right trail used for the animation
	rightTrail.set_process(true)
	rightTrail.show()

func enableShip():
	print("Enabled")
	set_process(true)

func disableShip():
	print("Disabled")
	set_process(false)