import React, { Component } from "react";
import UploadService from "../services/fileUpload";
import UploadFiles from "./fileUploadC"
import { useHistory , useNavigate, Link, BrowserRouter, Route, Routes, Router } from "react-router-dom";


export default class FileDetails extends Component {
    
    constructor(props) {
        super(props);

        const fileId = window.location.pathname.split('/')[2];
        
        this.state = {
            dataFileInfo: [],
            dataCountriesInfo: [],
            message: "",
            selectedFileId: fileId,
        };
    }

    
    deleteFile = (e) => {

        UploadService.delete(this.state.selectedFileId).then((response) => {
            this.setState({
                message: response.data,
            });
            
        });


    }


    componentDidMount() {
        UploadService.getFileById(this.state.selectedFileId).then((response) => { // get the files with countries
            this.setState({
                dataFileInfo: response.data,
                dataCountriesInfo: response.data[0].countries,
            });
        });
    }


    render() {
        const {
            selectedFileId,
            message,
            dataFileInfo,
            dataCountriesInfo
        } = this.state;

        return (
            <div>
                <a href="/">Home</a>

                <div className="alert alert-light" role="alert">
                    {message}
                </div>

                <div>
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    {Array.isArray(dataFileInfo) && dataFileInfo &&
                        dataFileInfo.map((file, index) => (
                            <li className="list-group-item" key={index}>
                                <div><h3>{file.idfile} {file.name} </h3></div>
                                <div><h5> {file.timestamp}</h5></div>
                            </li>
                        ))}
                </div>
                <div>
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    {Array.isArray(dataCountriesInfo) && dataCountriesInfo &&
                        dataCountriesInfo.map((file, index) => (
                            <li className="list-group-item" key={index}>
                                <div>{file.name} {file.value} {file.color}</div>
                            </li>
                        ))}
                </div>
                <button type="button" class="btn btn-primary btn-sm">Download txt</button>
                <button type="button" href="/" onClick={this.deleteFile} class="btn btn-secondary btn-sm">Delete</button>
            </div>
        );
    }
}