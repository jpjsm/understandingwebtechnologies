import socket
from flask import Flask
app = Flask(__name__)

@app.route("/", defaults={"path": ""})
@app.route("/<string:path>")
@app.route("/<path:path>")
def hello(path):
    hostname = socket.gethostname()
    ipaddress = socket.gethostbyname(hostname)
    return "[2.0.1]\nHost info: {0} ({1})\nRequested path: {2}".format(hostname, ipaddress, path)
if __name__ == '__main__':
    app.run(host ='0.0.0.0', port = 5000, debug = True)  