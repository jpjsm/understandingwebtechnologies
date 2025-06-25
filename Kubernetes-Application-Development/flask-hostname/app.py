import socket
from flask import Flask
app = Flask(__name__)

@app.route('/')
def hello():
    hostname = socket.gethostname()
    ipaddress = socket.gethostbyname(hostname)
    return "[1.0.2]{0}: {1}".format(hostname, ipaddress)
if __name__ == '__main__':
    app.run(host ='0.0.0.0', port = 5000, debug = True)  