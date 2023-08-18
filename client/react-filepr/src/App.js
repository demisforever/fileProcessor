import React, { Component } from 'react';
import './App.css';
import "bootstrap/dist/css/bootstrap.min.css";
import UploadFiles from "./components/fileUploadC";

function App() {
  return (
    <div className="container" style={{ width: "600px" }}>
      <UploadFiles />
    </div>
  );
}

export default App;
