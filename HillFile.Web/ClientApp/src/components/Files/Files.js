import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import {HubConnectionBuilder, LogLevel} from "@aspnet/signalr";
import { Table } from 'reactstrap';

export class Files extends Component {
    static displayName = Files.name;

    constructor(props) {
        super(props);
        
        this.state = {
            files: [],
            dir: this.props.location.pathname.replace("/files", ""),
            connection: null,
        };
        
        this.listFiles.bind(this);
    }

    componentDidMount() {
        const connection = new HubConnectionBuilder()
            .withUrl("/hubs/fileListing")
            .configureLogging(LogLevel.Information)
            .build();
        
        this.setState({
            connection: connection
        });

        connection.start().then(() => {
            this.listFiles();
        });

        connection.on("FileListing", (data) => {
            this.setState({
                files: [...this.state.files, data]
            });
        })
    }
    
    componentDidUpdate(prevProps, prevState, snapshot) {
        if (prevProps.location.pathname !== this.props.location.pathname) {
            this.setState({
                files: []
            });

            this.listFiles();
        }
    }
    
    listFiles() {
        this.setState({
            files: []
        });
        
        const connection = this.state.connection;

        connection.invoke("ListDirectory", this.state.dir);
    }

    static getDerivedStateFromProps(props, state) {
        if (props.location.pathname.replace("/files", "") !== state.dir) {
            return {
                dir: props.location.pathname.replace("/files", ""),
                files: [],
            }
        }
        
        return null;
    }

    render() {
        console.log(this.state);

        var directories = this.state.files.filter(file => file.type === 1);
        var files = this.state.files.filter(file => file.type === 0);
        
        return (
            <div>
                <h1>Files.js {this.state.dir}</h1>
                <Table striped hover responsive size={'sm'}>
                    <thead>
                        <tr>
                            <th scope={'head'}>Type</th>
                            <th scope={'head'}>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                    {directories.map(f => (
                        <tr>
                            <td>Dir</td>
                            <td><Link to={'/files/'+f.fullPath}>{f.name}</Link></td>
                        </tr>
                    ))}
                    {files.map(f => (
                        <tr>
                            <td>File</td>
                            <td><a href={'/api/file/filestream?file='+encodeURIComponent(f.fullPath)}>{f.name}</a></td>
                        </tr>
                    ))}
                    </tbody>
                </Table>
            </div>
        );
    }
}
