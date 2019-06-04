# Standard Imports
import os
import re
import math
import sys

# Global Variables
fin = open('base.pov', 'r+')
pov = fin.read() # Read the entire file into a string
fin.close()
povObject = pov

# Keyword argument requirement
screenshots = 50

# Dictionary Requirement
texture = {
    "pigment": "color rgb<1,1,1>",
    "normal": "bumps 0.5 scale 0.0125",
    "finish": "phong 1 reflection{ 0.15 }"
}

# Tuple Requirement
ovusSize = ( 1.00, 0.65 )

# List Requirement
ovusTranslate = [2,0.5,0]


class ovus(object):
    def addOvus():

        global pov
        global povObject
		# Ensure ovus does not exist
        povObject = re.sub(r'\bovus{ \b.+\}', " ", povObject)

		# Create ovus
        ovus = "ovus{ " + str(ovusSize[0]) + ", " + str(ovusSize[1])+ "texture{ pigment{" + texture["pigment"] + "} normal{" + texture["normal"] + "} finish {" + texture["finish"]+ "} } scale 0.5 rotate<0,0,0> translate<" + str(ovusTranslate[0]) + ", " + str(ovusTranslate[1]) + ", " + str(ovusTranslate[2]) + "> }"

		#Combine inital file and add in ovus object
        povObject = pov + ovus

    def moveObject(screenshots):
        global povObject
        global ovusTranslate
        x = screenshots
        radius = 2
        degrees = 3


		#### Part 2 object adding Requirement #####
        for i in range(x):
            if i == 4:
                ovus.addOvus()
            # Move ovus after 10 frames
            if i >= 10:
                ovusTranslate[0] = (radius * math.cos(degrees * (math.pi/180)))
                ovusTranslate[2] = (radius * math.sin(degrees * (math.pi/180)))
                degrees += 3.6
	            #update the replacement string with new values
                replace = "ovus{ " + str(ovusSize[0]) + ", " + str(ovusSize[1])+ "texture{ pigment{" + texture["pigment"] + "} normal{" + texture["normal"] + "} finish {" + texture["finish"]+ "} } scale 0.5 rotate<0,0,0> translate<" + str(ovusTranslate[0]) + ", " + str(ovusTranslate[1]) + ", " + str(ovusTranslate[2]) + "> }"

			    #replace the camara string with updated camara string
                povObject = re.sub(r'\bovus{ \b.+\}', replace, povObject)

			# create new pov file & start from beginning
            fout = open('temp2.pov', 'w')
            fout.seek(0)

			# write updated location to temp.pov
            fout.write(povObject)
            fout.close()

				# Open POV and render image
            cmd = 'pvengine.exe /RENDER temp2.pov +H600 +W800 -O"prt2_' + str(i) + '.png" /exit'
            os.system(cmd)

def moveCamera(screenshots):
    global pov
    radius = 15
    degrees = 3
    height = 10


    for i in range(screenshots):
		#### Part 1 Camara Pan Requirement #####
		# Pan the camera in a helix (god help me if you want me to make sense of this)
        x_cam = (radius * math.cos(degrees * (math.pi/180)))
        z_cam = (radius * math.sin(degrees * (math.pi/180)))
        height += .05
        degrees += 3.6
        #update the replacement string with new values
        replace = 'camera { perspective location  <' + str(x_cam) + ', ' + str(height) + ', ' + str(z_cam) + '> angle 25 look_at <0.0, 0.5, 0.0> }'
		#replace the camara string with updated camara string
        pov = re.sub(r'\bcamera { \b.+\}', replace, pov)

		# create new pov file & start from beginning
        fout = open('temp.pov', 'w')
        fout.seek(0)

		# write updated location to temp.pov
        fout.write(pov)
        fout.close()

		# Open POV and render image
        cmd = 'pvengine.exe /RENDER temp.pov +H600 +W800 -O"tmp' + str(i) + '.png" /exit'
        os.system(cmd)


def main():
	# move the camera
    moveCamera(15)
	# move the obect
    ovus.moveObject(15)

	# convert part 2 images into mp4 movie file
    os.system('ffmpeg -r 30 -i prt2_%d.png -vcodec libx264 -crf 25 -pix_fmt yuv420p movie2.mp4')
	# convert images into mp4 movie file
    os.system('ffmpeg -r 30 -i tmp%d.png -vcodec libx264 -crf 25 -pix_fmt yuv420p movie.mp4')

    print("It's done!")

main()
