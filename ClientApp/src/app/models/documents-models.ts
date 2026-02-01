export enum DocumentStatus {
    Active = 1,
    Expired,
}

export interface DocumentListItem {
    id: string;
    name: string;
    description: string;
    createdAt: string;
    expirationDate: string;
}

export interface DocumentDetails {
    id: string;
    name: string;
    description: string;
    expirationDate: string;
    latitude?: number;
    longitude?: number;
}

export interface UpdateDocumentModel {
    name: string;
    description: string;
    expirationDate: string;
    latitude?: number;
    longitude?: number;
}

export interface CreateDocumentModel {
    name: string;
    description: string;
    expirationDate: string;
    latitude?: number;
    longitude?: number;
}