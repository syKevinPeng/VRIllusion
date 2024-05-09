from pathlib import Path
from PIL import Image
import numpy as np

class OuchiLength:
    def __init__(self, width=512, height=512, radius=150, init_pattern_height=8, init_pattern_width=32, step_size=0.1):
        self.width = width
        self.height = height
        self.radius = radius
        self.init_pattern_height = init_pattern_height
        self.init_pattern_width = init_pattern_width
        self.current_pattern_height = init_pattern_height
        self.current_pattern_width = init_pattern_width
        self.step_size = step_size
        self.texture = self.generate_pattern()

    def generate_pattern(self):
        pattern_height = self.current_pattern_height
        pattern_width = self.current_pattern_width

        image = Image.new("RGB", (self.width, self.height), "white")
        pixels = image.load()

        # Define the background pattern
        for h in range(self.height):
            for w in range(self.width):
                if (h // pattern_height) % 2 == (w // pattern_width) % 2:
                    pixels[w, h] = (255, 255, 255)  # White
                else:
                    pixels[w, h] = (0, 0, 0)  # Black

        # Define the center circular pattern rotated 90 degrees clockwise
        center_x = self.width // 2
        center_y = self.height // 2
        for h in range(self.height):
            for w in range(self.width):
                if (w - center_x)**2 + (h - center_y)**2 < self.radius**2:
                    # Use w for vertical and h for horizontal checking, swapping their roles
                    if (w // pattern_height) % 2 == (h // pattern_width) % 2:
                        pixels[w, h] = (0, 0, 0)  # Black
                    else:
                        pixels[w, h] = (255, 255, 255)  # White

        return image

    def save_image(self, path):
        self.texture.save(path)
        print(f"Image saved to {path}")

    def set_pattern_ratio(self, ratio):
        adjusted_width = self.current_pattern_height * ratio
        if adjusted_width < self.current_pattern_width:
            self.current_pattern_width = min(int(adjusted_width), self.current_pattern_width - 1)
        elif adjusted_width > self.current_pattern_width:
            self.current_pattern_width = max(int(adjusted_width), self.current_pattern_width + 1)
        self.texture = self.generate_pattern()  # Update the texture with new ratio

    def increase_pattern_ratio(self):
        self.set_pattern_ratio(self.get_current_ratio() + self.step_size)
        self.texture = self.generate_pattern()

    def decrease_pattern_ratio(self):
        self.set_pattern_ratio(self.get_current_ratio() - self.step_size)
        self.texture = self.generate_pattern()

    def get_current_ratio(self):
        return self.current_pattern_width / self.current_pattern_height


numbers = np.arange(0.4, 4.1, 0.1) 

# Create an instance of OuchiLength
illusion = OuchiLength()
OUTPUT_DIR_PATH = "E:/Program Files/Unity/Unity Projects/VRIllusion/Assets/SavedTextures"
OUTPUT_DIR_PATH = Path(OUTPUT_DIR_PATH)
if not OUTPUT_DIR_PATH.exists():
    Exception(f"Directory {OUTPUT_DIR_PATH} does not exist")

for i in range(len(numbers)):
    # Increase the pattern ratio, show the updated texture, and save it
    illusion.increase_pattern_ratio()
    # illusion.texture.show()
    illusion.save_image(OUTPUT_DIR_PATH/"ouchi_illusion_length/{}.png".format(round(numbers[i], 2)))