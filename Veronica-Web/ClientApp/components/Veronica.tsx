import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface Message {
    isUser: boolean,
    message: string,
}

interface VeronicaResponse {
    response: string,
}

interface VeronicaState {
    messages: Message[];
    ok: boolean;
}

export class Veronica extends React.Component<RouteComponentProps<{}>, VeronicaState> {
    constructor() {
        super();
        this.state = {
            messages: [
                {isUser: false, message: 'Hi, how can I help you?'}
            ], ok: false
        };
        this.processUserQuery = this.processUserQuery.bind(this);
        this.processResponse = this.processResponse.bind(this);
        

        fetch('http://localhost:5000/ok')
            .then(response => {
                this.setState({ ok: true });
                console.log("Veronica is OK");
            })
    }

    public addMessage(message: Message) {
        let { state } = this;
        state.messages.push(message);
        this.setState({ messages: state.messages });
    }

    public processResponse(response: Response) {
        var message: Message = { isUser: false, message: 'If you see this, you failed lol' };

        response.json().then((json: VeronicaResponse) => {
            message = { isUser: false, message: json.response };
            this.addMessage(message);
        });


    }

    public processUserQuery(value: string) {
        this.addMessage({ isUser: true, message: value });
        const { processResponse } = this;

        fetch('/api/Query/Ask', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                UserInput: value
            })
        }).then(processResponse);
    }

    public onKeyPress(event: React.KeyboardEvent<HTMLInputElement>) {
        const { key } = event;
        const { processUserQuery } = this;
        

        if (key == 'Enter') {
            var queryElem = document.getElementById("query") as HTMLInputElement;
            console.log('user:', queryElem.value);

            processUserQuery(queryElem.value);

            queryElem.value = "";
        }
    }

    public getMessages() {
        return this.state.messages.map(message => {
            if (message.isUser) {
                return <div className="speech user">
                    <div className="speech-name">You</div>
                    <div className="speech-bubble-user">{message.message}</div>
                </div>;
            }
            else {
                return <div className="speech bot">
                    <div className="speech-name">Veronica</div>
                    <div className="speech-bubble-bot">
                    {message.message}
                    </div>
                </div>;
            }
        });
    }

    public render() {
        if (!this.state.ok) {
            return <h1>Waiting for Veronica...</h1>;
        }
        else {
            return <div className="userinput">
                <input type="text" className="form-control" id="query" placeholder="Enter query" onKeyPress={this.onKeyPress.bind(this)} />
                {this.getMessages()}
            </div>;
        }
    }

    /*incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }*/
}
