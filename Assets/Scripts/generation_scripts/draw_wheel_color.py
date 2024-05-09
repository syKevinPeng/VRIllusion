import math
from PIL import Image, ImageDraw
import numpy as np
from tqdm import tqdm

def rotate_point(center, point, angle):
    """Rotate a point counterclockwise by a given angle around a given center."""
    ox, oy = center
    px, py = point
    qx = ox + math.cos(angle) * (px - ox) - math.sin(angle) * (py - oy)
    qy = oy + math.sin(angle) * (px - ox) + math.cos(angle) * (py - oy)
    return qx, qy

def draw_rotated_square(draw, center, size, angle, shift=-12, border_colors=None):
    if border_colors is None:
        border_colors = ['#ffffff', '#ffffff', '#000000', '#000000']
    half_size = size / 2
    corners = [
        (center[0] - half_size + shift, center[1] - half_size),
        (center[0] + half_size + shift, center[1] - half_size),
        (center[0] + half_size - shift, center[1] + half_size),
        (center[0] - half_size - shift, center[1] + half_size)
    ]
    rotated_corners = [rotate_point(center, corner, angle) for corner in corners]
    rotated_corners.append(rotated_corners[0])

    border_width = 1
    for i in range(4):
        draw.line([rotated_corners[i], rotated_corners[i+1]], fill=border_colors[i], width=border_width)

def draw_reverse_rotated_square(draw, center, size, angle, shift=12, border_colors=None):
    if border_colors is None:
        border_colors = ['#ffffff', '#000000', '#000000', '#ffffff']
    half_size = size / 2
    corners = [
        (center[0] - half_size + shift, center[1] - half_size),
        (center[0] + half_size + shift, center[1] - half_size),
        (center[0] + half_size - shift, center[1] + half_size),
        (center[0] - half_size - shift, center[1] + half_size)
    ]
    rotated_corners = [rotate_point(center, corner, angle) for corner in corners]
    rotated_corners.append(rotated_corners[0])

    border_width = 1
    for i in range(4):
        draw.line([rotated_corners[i], rotated_corners[i+1]], fill=border_colors[i], width=border_width)

def draw_dot(draw, location, diameter, fill_color):
    top_left = (location[0] - diameter // 2, location[1] - diameter // 2)
    bottom_right = (location[0] + diameter // 2, location[1] + diameter // 2)
    draw.ellipse([top_left, bottom_right], fill=fill_color, outline=fill_color)

def blend_to_background(color, background, blend_ratio):
    from PIL import ImageColor
    color = ImageColor.getrgb(color)
    background = ImageColor.getrgb(background)
    return tuple(int(c + (b - c) * blend_ratio) for c, b in zip(color, background))

def draw_wheel(shift=100):
    original_width = 2048
    original_height = 2048
    image = Image.new('RGB', (original_width, original_height), color='grey')
    draw = ImageDraw.Draw(image)

    center_x, center_y = original_width // 2, original_height // 2
    num_squares = 36
    radius = 500
    square_size = 60
    dot_diameter = 32
    dot_color = 'black'
    grey_background = '#808080'

    for i in range(num_squares):
        angle = 2 * math.pi * i / num_squares
        square_x = center_x + radius * math.cos(angle)
        square_y = center_y + radius * math.sin(angle)
        border_colors = [
            blend_to_background('#ffffff', grey_background, shift / 100),
            blend_to_background('#ffffff', grey_background, shift / 100),
            blend_to_background('#000000', grey_background, shift / 100),
            blend_to_background('#000000', grey_background, shift / 100)
        ]
        draw_rotated_square(draw, (square_x, square_y), square_size, angle + math.pi/2, border_colors=border_colors)

    # Repeat for the inner circle using the reversed function
    num_squares = 30
    radius = 400
    for i in range(num_squares):
        angle = 2 * math.pi * i / num_squares
        square_x = center_x + radius * math.cos(angle)
        square_y = center_y + radius * math.sin(angle)
        border_colors = [
            blend_to_background('#ffffff', grey_background, shift / 100),
            blend_to_background('#000000', grey_background, shift / 100),
            blend_to_background('#000000', grey_background, shift / 100),
            blend_to_background('#ffffff', grey_background, shift / 100)
        ]
        draw_reverse_rotated_square(draw, (square_x, square_y), square_size, angle + math.pi/2, border_colors=border_colors)

    draw_dot(draw, (center_x, center_y), dot_diameter, dot_color)

    image.save('./wheel_illusion_color/{}.png'.format(shift), 'PNG')


numbers = np.arange(0, 100.1, 0.1) 
print(len(numbers))
for number in tqdm(numbers):
    draw_wheel(shift= round(number, 1))
