from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import urlparse
import json
import const
from rasa_nlu.model import Interpreter

interpreter = Interpreter.load(const.MODEL_DIRECTORY)

class ChatBotServer(BaseHTTPRequestHandler):
    """
    The AI API server for handling RASA requests
    """

    def get_ok(self, params):
        self.send_response(200)
        self.send_header('Content-Type', 'text/json')
        self.send_header('Access-Control-Allow-Origin', '*')
        self.end_headers()

        return json.dumps({"Ok": True});   

    def get_test(self, params):
        """
        End point for POST /parse
        """

        if const.QUERY_PARAM in params:
            self.send_response(200)
            self.send_header('Content-Type', 'text/json')
            self.end_headers()

            # run the interpreter to get intents/entities from the question  
            results = interpreter.parse(params[const.QUERY_PARAM])  

            # output the resulting intents/entities of the query asked by the user     
            return json.dumps(results)
        else:
            self.send_error(404)


    def post_parse(self, params):
        """
        End point for POST /parse
        """

        if const.QUERY_PARAM in params:
            self.send_response(200)
            self.send_header('Content-Type', 'text/json')
            self.send_header('Access-Control-Allow-Origin', '*')
            self.end_headers()

            # run the interpreter to get intents/entities from the question  
            results = interpreter.parse(params[const.QUERY_PARAM])  

            # output the resulting intents/entities of the query asked by the user     
            return json.dumps(results)
        else:
            self.send_error(404)

    def do_GET(self):
        """
        Responds to GET requests
        """
        url = urlparse(self.path)
        path =  url.path[1:]
        method_name = 'get_' + path

        if (method_name in dir(self)):
            print("GET", path, "CALL", method_name)
            method = getattr(self, method_name)
            self.wfile.write(bytes(method(url.params), "utf8"))
        else:
            self.send_response(200)
            self.send_header('Content-Type', 'text/html')
            self.end_headers()
            self.wfile.write(bytes('<h1>Hello, World!</h1><p>If you\'re seeing this, Veronica is awake.</p>', "utf8"))

    def do_POST(self):
        """
        Responds to POST requests
        """
        url = urlparse(self.path)
        path =  url.path[1:]
        method_name = 'post_' + path

        if (self.headers['Content-Type'] == "application/json"):
            raw_data = self.rfile.read(int(self.headers['Content-Length']))
            data = json.loads(raw_data)
            if (method_name in dir(self)):
                print("POST", path, "CALL", method_name)
                method = getattr(self, method_name)
                self.wfile.write(bytes(method(data), "utf8"))
            else:
                self.send_error(404, "Function "+path+" not defined")
        else:
            self.send_error(404, "Not Found")

    
def run():
    """
    Main function for running the bot
    """
    server_address = ('', 5000)
    httpd = HTTPServer(server_address, ChatBotServer)
    httpd.serve_forever()

run() 