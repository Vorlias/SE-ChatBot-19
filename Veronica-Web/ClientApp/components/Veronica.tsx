import * as React from 'react';
import { RouteComponentProps } from 'react-router';

/**
 * A message in the chat
*/
interface Message {
    /**
     * Whether or not the message is from the user
     */
    isUser: boolean,

    /**
     * The message
     */
    message: string,
}

/**
 * A response from veronica
 */
interface VeronicaResponse {
    response: string,
}

/**
 * The state of veronica
 */
interface VeronicaState {
    messages: Message[];
    ok: boolean;
}

/**
 * The component for the Veronica chat interface 
 */
export class Veronica extends React.Component<RouteComponentProps<{}>, VeronicaState> {
    constructor() {
        super();
        this.state = {
            messages: INITIAL_MESSAGES, ok: false
        };
        this.processUserQuery = this.processUserQuery.bind(this);
        this.processResponse = this.processResponse.bind(this);
        
        // We check if Veronica is active first, if so we make the component active.
        fetch('http://localhost:5000/ok')
            .then(response => {
                this.setState({ ok: true });
                console.log("Veronica is OK");
            })
    }

    /**
     * Force the chat to scroll down
     */
    public forceScroll() {
        var scroll = document.getElementById('user-chat') as HTMLElement;
        scroll.scrollTo(0, scroll.scrollHeight + 100);
    }

    /**
     * Add a message to the chat
     * @param message The message to add
     */
    public addMessage(message: Message) {
        let { state } = this;
        state.messages.push(message);
        this.setState({ messages: state.messages });
    }

    /**
     * Process the response from our chat bot
     * @param response The response from the chat bot request
     */
    public processResponse(response: Response) {
        var message: Message = { isUser: false, message: 'If you see this, you failed lol' };

        response.json().then((json: VeronicaResponse) => {
            message = { isUser: false, message: json.response };
            this.addMessage(message);
            this.forceScroll();
        });
    }

    /**
     * Process a user's query
     * @param query The query from the user
     */
    public processUserQuery(query: string) {
        // Add the user's message to the message list
        this.addMessage({ isUser: true, message: query });
        this.forceScroll();

        const { processResponse } = this;

        // Send a request to the server for a response to the user query
        fetch('/api/Query/Ask', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                UserInput: query
            })
        }).then(processResponse);
    }

    /**
     * Handles the enter key press from the text box
     */
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

    /**
     * Get the current message list to render
    */
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

    /**
     * Rendering for the interface
     */
    public render() {
        if (!this.state.ok) {
            return <h1>Waiting for Veronica...</h1>;
        }
        else {
            return <div>
                <div className="user-chat" id="user-chat">
                    {this.getMessages()}
                </div>
                <div className="user-input">
                    <input type="text" className="form-control" id="query" placeholder="Enter query" onKeyPress={this.onKeyPress.bind(this)} />

                </div>
            </div>;
        }
    }
}

/**
 * The initial messages to display
*/
const INITIAL_MESSAGES: Message[] = [
    { isUser: false, message: "How can I help you?" },
]