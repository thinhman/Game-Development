extends Node

var alien_location = 0.0;
var curve_num = 0;
var timer = 0.0;
export var timer_cap = 10.0;
export var timer_offset = 0.0;

var alien_speed = 400;
export var reload_time = 0.5;

var reload_timer = 0.0;
export var random_shoot_var = 1001

const laser_scn = preload("res://enemyLaser.tscn")

func _ready():
	# Wait for an additional 3 seconds for the animation to play
	timer_offset += 3.0
	$alien_path.set_curve($intro_path.get_curve());
	pass

func shoot():
	if !get_node("/root/Root/GameManager").can_shoot:
		return
	var laser = laser_scn.instance();
	var num;
	randomize()
	#rand_range(1, 101):
	num = rand_range(1, random_shoot_var);
	if(num <= 3.0 and reload_timer <= 0.0):
		laser.position = Vector2($alien_path/PathFollow2D.position.x, $alien_path/PathFollow2D.position.y);
		laser.shooter = $alien_path/PathFollow2D/alien.name;
		laser.velocity.y = 600;
		reload_timer = reload_time;
		get_tree().root.add_child(laser)
		
func _process(delta):
	if(timer_offset < 0.0):
		if(curve_num != 1 and $alien_path/PathFollow2D.unit_offset < 1.0):
			$alien_path/PathFollow2D.offset = alien_location;
			alien_location += alien_speed * delta;
			shoot();
		elif(curve_num == 0 and $alien_path/PathFollow2D.unit_offset >= 1.0):
			$alien_path.set_curve($idle_path.get_curve());
			$alien_path/PathFollow2D.rotate = false;
			$alien_path/PathFollow2D.rotation = 0.0;
			$alien_path/PathFollow2D/alien.rotation_degrees = 0;
			alien_speed = 50;
			alien_location = 0;
			curve_num = 1;
		elif(curve_num == 1 and timer < timer_cap):
			timer += delta;
			if($alien_path/PathFollow2D.unit_offset < 1.0):
				$alien_path/PathFollow2D.offset = alien_location;
				alien_location += alien_speed * delta;
				shoot();
			else:
				alien_location = 0.0;
				$alien_path/PathFollow2D.unit_offset = 0;
			
		elif(curve_num == 1 and timer >= timer_cap):
			$alien_path.set_curve($leaving_path.get_curve());
			$alien_path/PathFollow2D.rotate = true;
			$alien_path/PathFollow2D/alien.rotation_degrees = -90;
			alien_speed = 500;
			timer = 0.0;
			alien_location = 0.0;
			curve_num = 2;
		elif(curve_num == 2):
			$alien_path.set_curve($return_path.get_curve());
			alien_speed = 100;
			alien_location = 0.0;
			$alien_path/PathFollow2D.unit_offset = 0;
			timer_cap = 5.0;
			curve_num = 3
		elif(curve_num == 3):
			$alien_path.set_curve($idle_path.get_curve());
			$alien_path/PathFollow2D.rotate = false;
			$alien_path/PathFollow2D.rotation = 0.0;
			$alien_path/PathFollow2D/alien.rotation_degrees = 0;
			alien_speed = 50;
			curve_num = 1;
		reload_timer -= delta;
	else:
		timer_offset -= delta;
	pass
