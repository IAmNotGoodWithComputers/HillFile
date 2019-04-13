import React, {Component} from 'react';
import {HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import axios from 'axios';
import queryString from 'query-string';
import { Table } from 'reactstrap';
import { NavMenu} from "./NavMenu";


export class Home extends Component {
    static displayName = Home.name;

    static DirectoryType = 1;
    static FileType = 0;

    constructor(props) {
        super(props);

        this.state = {
            files: [],
            dir: ''
        }
    }
    
    componentDidMount() {
        const connection = new HubConnectionBuilder()
            .withUrl("/hubs/fileListing")
            .configureLogging(LogLevel.Information)
            .build();

        this.setState({
            connection: connection
        });
        
        let params = queryString.parse(window.location.search);
        if (typeof params.dir !== typeof undefined) {
            this.setState({
                dir: params.dir
            });

            connection.start().then(() => {
                this.listFiles();
            });
        } else {
            // @todo for development purposes only, start with the home directory of the current user
            // under Linux, this is the home folder
            // under Windows, this is the Documents folder
            axios.get('/api/user/homedir').then((response) => {
                this.setState({
                    'dir': response.data
                });

                connection.start().then(() => {
                    this.listFiles();
                });
            });
        }
    }

    listFiles(dir) {
        console.log(this.state);
        this.setState({
            files: []
        });
        
        if (typeof dir === typeof undefined || dir == null) {
            dir= this.state.dir;
        }

        const connection = this.state.connection;
        connection.invoke("ListDirectory", dir);
        
        connection.on("FileListing", (data) => {
            this.setState({
                files: [...this.state.files, data]
            });
        })
    }

    render() {
        console.log("render called");
        return (
            <div>
                <Table striped bordered hover size={'sm'}>
                    <thead>
                        <tr>
                            <td>Type</td>
                            <td>Name</td>
                        </tr>
                    </thead>
                    <tbody>
                    {this.state.files.filter(file => file.type === Home.DirectoryType).map(file => (
                        <tr key={file.fullPath} className={"table-row"}>
                            <td>
                                {file.type === 1 ? "Dir" : "File"}
                            </td>
                            {/* I'm sure this is not supposed to be done like that */}
                            <td><a href={'?dir='+encodeURIComponent(file.fullPath)}>{file.name}</a></td>
                        </tr>
                    ))}

                    {this.state.files.filter(file => file.type === Home.FileType).map(file => (
                        <tr key={file.fullPath} className={"table-row"}>
                            <td>
                                {file.type === 1 ? "Dir" : "File"}
                            </td>
                            <td><a href={'/api/file/filestream?file='+encodeURIComponent(file.fullPath)}>{file.name}</a></td>
                        </tr>
                    ))}
                    </tbody>
                </Table>
            </div>
        );
    }
}
