import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateDocumentModel, DocumentDetails, DocumentListItem, UpdateDocumentModel } from "../models/documents-models";

@Injectable({
    providedIn: 'root'
})
export class DocumentsService {
    private apiUrl = 'http://localhost:5000/api/documents';

    constructor(private http: HttpClient) { }

    getDocuments(): Observable<DocumentListItem[]> {
        return this.http.get<DocumentListItem[]>(`${this.apiUrl}`);
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
}