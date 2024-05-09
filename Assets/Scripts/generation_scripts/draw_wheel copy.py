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

def draw_rotated_square(draw, center, size, angle, shift=-8, border_colors = ['white', 'white', 'black', 'black']):
    half_size = size / 2
    # Define the corners of the parallelogram relative to the center
    # Shift the top and bottom sides horizontally to create the parallelogram effect
    corners = [
        (center[0] - half_size + shift, center[1] - half_size),                    # top-left
        (center[0] + half_size + shift, center[1] - half_size),                    # top-right
        (center[0] + half_size - shift, center[1] + half_size),                    # bottom-right
        (center[0] - half_size - shift, center[1] + half_size)                     # bottom-left
    ]

    # Rotate each corner
    rotated_corners = [rotate_point(center, corner, angle) for corner in corners]

    # Append first corner at the end to complete the parallelogram loop
    rotated_corners.append(rotated_corners[0])

    # Draw lines for each edge with specified colors
      # Customize as needed
    border_width = 1  # Set the width of the border lines

    for i in range(4):
        draw.line([rotated_corners[i], rotated_corners[i+1]], fill=border_colors[i], width=border_width)

def draw_reverse_rotated_square(draw, center, size, angle, shift=8, border_colors = ['white', 'black', 'black', 'white']):
    half_size = size / 2
    # Define the corners of the parallelogram relative to the center
    # Shift the top and bottom sides horizontally to create the parallelogram effect
    corners = [
        (center[0] - half_size + shift, center[1] - half_size),                    # top-left
        (center[0] + half_size + shift, center[1] - half_size),                    # top-right
        (center[0] + half_size - shift, center[1] + half_size),                    # bottom-right
        (center[0] - half_size - shift, center[1] + half_size)                     # bottom-left
    ]

    # Rotate each corner
    rotated_corners = [rotate_point(center, corner, angle) for corner in corners]

    # Append first corner at the end to complete the parallelogram loop
    rotated_corners.append(rotated_corners[0])

    # Draw lines for each edge with specified colors
      # Customize as needed
    border_width = 1  # Set the width of the border lines

    for i in range(4):
        draw.line([rotated_corners[i], rotated_corners[i+1]], fill=border_colors[i], width=border_width)

def draw_dot(draw, location, diameter, fill_color):
    # Calculate the top-left and bottom-right coordinates for the bounding box
    top_left = (location[0] - diameter // 2, location[1] - diameter // 2)
    bottom_right = (location[0] + diameter // 2, location[1] + diameter // 2)
    
    # Draw the ellipse (dot)
    draw.ellipse([top_left, bottom_right], fill=fill_color, outline=fill_color)

def draw_wheel(shift=100):
    # Create image and drawing context
    original_width = 2048
    original_height = 2048
    image = Image.new('RGB', (original_width, original_height), color='grey')
    draw = ImageDraw.Draw(image)

    # Parameters for circles and squares
    # Parameters for the dot
    center_x, center_y = original_width // 2, original_height // 2
    num_squares = 36
    radius = 500
    square_size = 60
    dot_diameter = 8  # Adjust the diameter of the dot as desired
    dot_color = 'black'  # Color of the dot


    # Draw rotated squares
    for i in range(num_squares):
        angle = 2 * math.pi * i / num_squares
        square_x = center_x + radius * math.cos(angle)
        square_y = center_y + radius * math.sin(angle)
        # Rotate square to align tangentially, angle needs adjustment by pi/2 to align properly
        draw_rotated_square(draw, (square_x, square_y), square_size, angle + math.pi/2,shift=shift)

    # Repeat for the inner circle
    num_squares = 30
    radius = 400
    for i in range(num_squares):
        angle = 2 * math.pi * i / num_squares
        square_x = center_x + radius * math.cos(angle)
        square_y = center_y + radius * math.sin(angle)
        draw_reverse_rotated_square(draw, (square_x, square_y), square_size, angle + math.pi/2, shift=shift)

    # Draw the dot at the center of the circle
    draw_dot(draw, (center_x, center_y), dot_diameter, dot_color)

    # Display the image
    # image.show()
    image.save('./wheel_illusion_color/{}.png'.format(shift), 'PNG')


numbers = np.arange(0, 0.2, 0.1) 
print(len(numbers))
for number in tqdm(numbers):
    draw_wheel(shift= round(number, 1))
