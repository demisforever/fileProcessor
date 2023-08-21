import React, { Component } from 'react';
import './App.css';
import "bootstrap/dist/css/bootstrap.min.css";
import UploadFiles from "./components/fileUploadC";
import { Link, BrowserRouter, Route, Routes, Router } from "react-router-dom";
import FileDetails from "./components/fileDetails";

function App() {
  return (
    <div className="container" style={{ width: "600px" }}>
      <BrowserRouter>
        <Routes>
          <Route exact path="/" element={<UploadFiles />}> </Route>
          <Route exact path="/fileDetails" element={<FileDetails />}> </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
