import socket
from naoqi import ALProxy

hostname = socket.gethostname()
ip = socket.gethostbyname(hostname)

tts = ALProxy("ALTextToSpeech", "10.0.1.37", 9559)
tts.say("Starting application on remote machine.")

host, port = "", 25001

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.bind((host, port))

tts.say("Listening for connections")
print 'Listening ...'

sock.listen(1)
conn, addr = sock.accept()

tts.say("Connected to Unity application.")
print 'Connected by', addr
		
while True:
	data = conn.recv(1024)
	if not data:
		break
	tts.say("Data is " + data)
	print 'Getting data: ', data
	tts.say(data)
	conn.sendall(data)
conn.close()
