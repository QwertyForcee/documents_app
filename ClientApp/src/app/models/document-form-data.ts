import { DocumentDetails } from "./documents-models";

export interface DocumentFormData {
    isReadOnly: boolean;
    documentDetails: DocumentDetails;
}