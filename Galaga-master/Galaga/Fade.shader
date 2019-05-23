shader_type canvas_item;

void fragment()
{
	COLOR = texture(TEXTURE, UV);
	COLOR.a = abs(sin(TIME));
	
	//For the black background get rid of it
	if(COLOR.rgb == vec3(0.0))
	{
		COLOR.a = 0.0;
	}	
}
