import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateDocumentModel, DocumentDetails, DocumentListItem, DocumentStatus, UpdateDocumentModel } from "../models/documents-models";
import { environment } from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class DocumentsService {
    private apiUrl = `${environment.apiUrl}/documents`;

    constructor(private http: HttpClient) { }

    getDocuments(status: DocumentStatus): Observable<DocumentListItem[]> {
        const params = new HttpParams()
            .set('status', status);

        return this.http.get<DocumentListItem[]>(`${this.apiUrl}`, { params });
    }

    getDocumentById(id: string): Observable<DocumentDetails> {
        return this.http.get<DocumentDetails>(`${this.apiUrl}/${id}`);
    }

    createDocument(dto: CreateDocumentModel): Observable<string> {
        return this.http.post<string>(this.apiUrl, dto);
    }

    updateDocument(id: string, dto: UpdateDocumentModel): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
    }

    deleteDocument(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }

    copyDocument(id: string): Observable<string> {
        return this.http.post<string>(`${this.apiUrl}/${id}/copy`, null);
    }
}