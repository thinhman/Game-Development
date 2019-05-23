shader_type canvas_item;

uniform float speed;

void fragment()
{
	vec2 pixel_offset = (UV + vec2(0.0,-TIME * speed));
	vec4 tex_color = texture(TEXTURE, pixel_offset);
	COLOR = tex_color;
}