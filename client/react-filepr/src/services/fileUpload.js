import http from "../http-common";
import UploadService from "../services/fileUpload";

class fileUploadDataService {
    getFiles() {
        return http.get("/file");
    }

    getFileById(id) {
        return http.get(`/file/${id}`);
    }
    
    upload(file, onUploadProgress) {
        let formData = new FormData();
        formData.append("Ffile", file);

        return http.post("/File/uploading", formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            },
            onUploadProgress,
        });
    }

    delete(id) {
        return http.delete(`/file/${id}`);
    }
}

export default new fileUploadDataService();