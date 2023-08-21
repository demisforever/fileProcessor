import React, { Component } from "react";
import UploadService from "../services/fileUpload";
import FileDetails from "./fileDetails";
import { Link, BrowserRouter, Route, Routes, Router } from "react-router-dom";

export default class UploadFiles extends Component {
    constructor(props) {
        super(props);

        this.state = {
            selectedFiles: undefined,
            currentFile: undefined,
            progress: 0,
            message: "",

            fileInfos: [],
        };
    }

    // get the selected Files from <input type="file" >
    selectFile = (event) => {
        this.setState({
            progress: 0,
            selectedFiles: event.target.files,
        });
    }

    upload = async () => {
        let currentFile = this.state.selectedFiles[0];

        this.setState({
            progress: 0,
            currentFile: currentFile,
        });

        UploadService.upload(currentFile, (event) => {
            this.setState({
                progress: Math.round((100 * event.loaded) / event.total), //calculate the progress
            });
        })
            .then((response) => {
                this.setState({
                    message: response.data,
                });
                return UploadService.getFiles(); // update fileInfos state
            })
            .then((files) => {
                this.setState({
                    fileInfos: files.data,
                });
            })
            .catch(() => {
                this.setState({
                    progress: 0,
                    message: "Could not upload the file!",
                    currentFile: undefined,
                });
            });

        this.setState({
            selectedFiles: undefined,
        });
    }

    componentDidMount() {
        UploadService.getFiles().then((response) => { // get the files information and assign the result to fileInfos state
            this.setState({
                fileInfos: response.data,
            });
        });
    }

    // user interface
    render() {
        const {
            selectedFiles,
            currentFile,
            progress,
            message,
            fileInfos,
        } = this.state;

        return (
            <div>
                {currentFile && (
                    <div className="progress">
                        <div
                            className="progress-bar progress-bar-info progress-bar-striped"
                            role="progressbar"
                            aria-valuenow={progress}
                            aria-valuemin="0"
                            aria-valuemax="100"
                            style={{ width: progress + "%" }}
                        >
                            {progress}%
                        </div>
                    </div>
                )}

                <label className="btn btn-default">
                    <input type="file" onChange={this.selectFile} />
                </label>

                <button className="btn btn-success"
                    disabled={!selectedFiles}
                    onClick={this.upload}
                >
                    Upload
                </button>

                <div className="alert alert-light" role="alert">
                    {message}
                </div>

                <div className="card">
                    <div className="card-header">List of Files</div>
                    <ul className="list-group list-group-flush">
                        {Array.isArray(fileInfos) && fileInfos &&
                            fileInfos.map((file, index) => (
                                <li className="list-group-item" key={index}>
                                    <a href="/fileDetails">{file.idfile} {file.name} {file.timestamp}</a>
                                </li>
                            ))}
                    </ul>
                </div>
                <div>
                </div>
            </div>
        );
    }

}