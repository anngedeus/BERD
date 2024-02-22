import socket
from naoqi import ALProxy

tts = ALProxy("ALTextToSpeech", "10.0.1.37", 9559)

host, port = "", 25000

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.bind((host, port))
print 'Listening ...'
sock.listen(1)
conn, addr = sock.accept()


print 'Connected by', addr
		
while True:
	data = conn.recv(1024)
	if not data:
		break
	print 'Getting data: ', data
	tts.say(data)
	conn.sendall(data)
conn.close()
