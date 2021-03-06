import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

/**
 * The navigation menu component for our chat bot
 */
export class NavMenu extends React.Component<{}, {}> {
    /**
     * Render the navigation menu
     */
    public render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={ '/' }>Veronica ChatBot</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink to={ '/' } exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/talk' } activeClassName='active'>
                                <span className='glyphicon glyphicon-comment'></span> Chat
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
