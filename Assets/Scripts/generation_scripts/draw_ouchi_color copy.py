import numpy as np
from PIL import Image
from tqdm import tqdm

class OuchiLength:
    def __init__(self, width=512, height=512, radius=150, init_pattern_height=8, init_pattern_width=32, step_size=0.01):
        self.width = width
        self.height = height
        self.radius = radius
        self.init_pattern_height = init_pattern_height
        self.init_pattern_width = init_pattern_width
        self.current_pattern_height = init_pattern_height  # Initialize current_pattern_height
        self.current_pattern_width = init_pattern_width    # Initialize current_pattern_width
        self.step_size = step_size
        self.blend_ratio = 0  # Initialize blend ratio for color transition
        self.texture = self.generate_pattern()

    def generate_pattern(self):
        pattern_height = self.current_pattern_height
        pattern_width = self.current_pattern_width

        image = Image.new("RGB", (self.width, self.height), "white")
        pixels = image.load()

        # Calculate colors based on the current blend ratio
        black = self.blend_colors((0, 0, 0), (128, 128, 128), self.blend_ratio)
        white = self.blend_colors((255, 255, 255), (128, 128, 128), self.blend_ratio)

        # Define the background pattern
        for h in range(self.height):
            for w in range(self.width):
                if (h // pattern_height) % 2 == (w // pattern_width) % 2:
                    pixels[w, h] = white
                else:
                    pixels[w, h] = black

        # Define the center circular pattern rotated 90 degrees clockwise
        center_x = self.width // 2
        center_y = self.height // 2
        for h in range(self.height):
            for w in range(self.width):
                if (w - center_x)**2 + (h - center_y)**2 < self.radius**2:
                    if (w // pattern_height) % 2 == (h // pattern_width) % 2:
                        pixels[w, h] = black
                    else:
                        pixels[w, h] = white

        return image

    def blend_colors(self, color1, color2, blend_ratio):
        """Blends color1 towards color2 based on blend_ratio (0 to 1)"""
        return tuple(int(c1 + (c2 - c1) * blend_ratio) for c1, c2 in zip(color1, color2))

    def save_image(self, path):
        self.texture.save(path)
        # print(f"Image saved to {path}")

    def set_color_blend_ratio(self, ratio):
        # Set the blend ratio for color transition
        self.blend_ratio = ratio
        self.texture = self.generate_pattern()  # Update the texture with new color settings

    def increase_pattern_ratio(self):
        new_ratio = self.blend_ratio + self.step_size
        self.set_color_blend_ratio(min(new_ratio, 1))  # Ensure the ratio does not exceed 1

    def decrease_pattern_ratio(self):
        new_ratio = self.blend_ratio - self.step_size
        self.set_color_blend_ratio(max(new_ratio, 0))  # Ensure the ratio does not fall below 0

# Create an instance of OuchiLength
illusion = OuchiLength()

numbers = np.arange(0.0, 5.1, 0.01) 

for i in tqdm(range(len(numbers))):
    # Increase the pattern ratio, update the blend ratio, and save the image
    illusion.increase_pattern_ratio()
    illusion.save_image(f"./ouchi_illusion_color/{round(illusion.blend_ratio, 2)}.png")
