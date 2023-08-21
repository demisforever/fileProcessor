import React, { Component } from "react";
import UploadService from "../services/fileUpload";

export default class UploadFiles extends Component {
    constructor(props) {
        super(props);

        this.state = {
            dataFileInfo: [],
            dataCountriesInfo: [],
            message: "",
            selectedFileId: 1,
        };
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
                <div>
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    {Array.isArray(dataFileInfo) && dataFileInfo &&
                        dataFileInfo.map((file, index) => (
                            <li className="list-group-item" key={index}>
                                <div>{file.idfile} {file.name} {file.timestamp}</div>
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
            </div>
        );
    }
}