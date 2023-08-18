import http from "../http-common";

class fileUploadDataService {
    getFiles() {
        return http.get("/file");
    }

    getAll() {
        return http.get("/file/all");
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