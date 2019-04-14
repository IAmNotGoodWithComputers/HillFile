import React, {Component} from 'react';

export class Home extends Component {
    static displayName = Home.name;

    static DirectoryType = 1;
    static FileType = 0;
    
    render() {
        console.log(this.state);
        return (
            <div>
                <h1>HillFile Dev</h1>
                <div>Welcome to HillFile. A Work-in-Progress Cloud storage system. Please note that right now,
                almost nothing is actually working! 
                    
                    <hr/>
                
                Thank you for contributing!</div>
            </div>
        );
    }
}
