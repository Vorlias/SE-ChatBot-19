import * as React from 'react';
import { RouteComponentProps } from 'react-router';

/**
 * The home component for the chat bot, used to display the welcome message.
 */
export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    /**
     * Render the home page
     */
    public render() {
        return <div>
            <h1>Welcome to Veronica!</h1>
            <p>
                This is our ChatBot prototype for Software Engineering.
            </p>
            <p>
                If you want to give her a try, just press the link on the left side.
            </p>
        </div>;
    }
}
