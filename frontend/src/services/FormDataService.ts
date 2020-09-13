import http from "../http-common";
import {FormFilter} from "@/filters/formFilter";

class FormDataService {
    getByFilter(filter:FormFilter) {
        return http.get(`/form/byFilter?take=${filter.take}&skip=${filter.skip}&title=${filter.title}`);
    }

    get(id: string) {
        return http.get(`/form/${id}`);
    }

    create(data: any) {
        return http.post("/form/create", data);
    }

    update(id: string, data: any) {
        return http.put(`/form/${id}`, data);
    }

    delete(id: string) {
        return http.delete(`/form/${id}`);
    }

    deleteAll() {
        return http.delete(`/form/batch`);
    }
}

export default new FormDataService();