# How to Run the Python Script Inside a Docker Container
*Note: These are the instructions I used to build and run the docker container. If you try to run the nao.py file outside of the docker
container it WILL NOT WORK.*

## Install Docker before you start
These commands are for the docker CLI so make sue you can use that before you start. If you get some permission denied errors when running docker
commands you need to run all docker commands as root (with sudo) or change the ownership of the docker command (look this up it's ez).

## Build the docker image
There's a Dockerfile in this folder that you can use to build an image. The container will come with the correct version of the python sdk
for naoqi, the correct python path, and python2 installed.

'''
docker image build -t nao .
'''

## Run the container
After the docker image has been successfully built you can run a container using the image. The Dockerfile specifies that, by default, the
will run the nao.py script.

'''
docker run -it nao
'''
