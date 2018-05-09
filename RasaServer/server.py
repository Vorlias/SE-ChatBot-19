from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import urlparse
import json
import const
from rasa_nlu.model import Interpreter

def get_parameters(query):
    parts = query.split("&")
    query = {}
    for value in parts:
        keyValue = value.split("=")
        query[keyValue[0]] = keyValue[1]

    return query

class ChatBotServer(BaseHTTPRequestHandler):
    """
    The AI API server for handling RASA requests
    """

    def parse(self, params):
        """
        End point for POST /parse
        """

        if 'Query' in params:
            self.send_response(200)
            self.send_header('Content-Type', 'text/json')
            self.end_headers()

            interpreter = Interpreter.load('./models/nlu/default/' + const.MODEL_NAME)
            results = interpreter.parse(params['Query'])       
            return json.dumps(results)
        else:
            self.send_error(404)

    def do_POST(self):
        url = urlparse(self.path)
        path =  url.path[1:]

        if (self.headers['Content-Type'] == "application/json"):
            raw_data = self.rfile.read(int(self.headers['Content-Length']))
            data = json.loads(raw_data)
            if (path in dir(self)):
                method = getattr(self, path)
                self.wfile.write(bytes(method(data), "utf8"))
            else:
                self.send_error(404, "Function "+path+" not defined")
        else:
            self.send_error(404, "Not Found")


    def do_GET(self):
        url = urlparse(self.path)
        path =  url.path[1:]
        params = get_parameters(url.query)

        print(json.dumps(params))
       
        if (path in dir(self)):
            method = getattr(self, path)
            self.wfile.write(bytes(method(params), "utf8"))
        elif (path == '/responsedd' and 'question' in params):
            self.send_response(200)
            self.send_header('Content-Type', 'text/json')
            self.end_headers()

            interpreter = Interpreter.load('./models/nlu/default/chat')
            results = interpreter.parse(params['question'])

            self.wfile.write(bytes(json.dumps(results), "utf8"))
        else:
            self.send_error(404, "Not Found")
            self.wfile.write(bytes(path, "utf8"))

        return

    
def run():
    """
    Main function for running the bot
    """
    server_address = ('127.0.0.1', 5000)
    httpd = HTTPServer(server_address, ChatBotServer)
    httpd.serve_forever()

run()